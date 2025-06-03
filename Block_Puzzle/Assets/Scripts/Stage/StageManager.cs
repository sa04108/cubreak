using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cubreak
{
    public class StageManager : MonoBehaviour
    {
        private GameObject cubeObject;

        private int nowStage;
        private int clearedStage;
        private int stageGroupIndex; // start with 0

        private CubeStage[] stageData;
        private int exerciseDimension;

        [SerializeField] private TextAsset stageJson;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private Transform cubeParent;
        [SerializeField] private GameObject[] cubePrefab;
        [SerializeField] private Button[] stageButtons;
        [SerializeField] private Button exerciseButton;
        [SerializeField] private Button prevButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button hintButton;
        [SerializeField] private Button seeThroughButton;

        private void Start()
        {
            // NOTE
            // https://docs.unity3d.com/6000.1/Documentation/ScriptReference/Application-targetFrameRate.html
            // Desktop 플래폼에서는 vSync = 0일때에만 Target FrameRate가 작동함
            // Android, IOS는 vSync를 항상 무시함
            // Desktop과 Mobile에서의 동일한 애니메이션 속도를 제공하기 위해 동일한 프레임으로 고정하는 것을 권장
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;

            stageData = CubeStage.FromJson(stageJson.text);
            clearedStage = CustomPlayerPrefs.GetInt(ENUM_PLAYERPREFS.ClearedStage, 0);
            stageGroupIndex = clearedStage / stageButtons.Length;

            InitializeStagePanel();

            exerciseButton.onClick.AddListener(() => StartStage(0));
            hintButton.onClick.AddListener(RevealHintBlocks);

            prevButton.onClick.AddListener(() =>
            {
                stageGroupIndex--;

                if (stageGroupIndex < 0)
                {
                    stageGroupIndex = 0;
                    return;
                }

                InitializeStagePanel();
            });

            nextButton.onClick.AddListener(() =>
            {
                stageGroupIndex++;

                int maxStageNum = stageData.Length;
                if (stageGroupIndex > (maxStageNum - 1) / stageButtons.Length)
                {
                    stageGroupIndex--;
                    return;
                }

                InitializeStagePanel();
            });
        }

        private void InitializeStagePanel()
        {
            int maxStageNum = stageData.Length;

            for (int i = 0; i < stageButtons.Length; i++)
            {
                int stageNum = (i + 1) + stageGroupIndex * stageButtons.Length;

                if (stageNum <= maxStageNum)
                {
                    stageButtons[i].gameObject.SetActive(true);
                    stageButtons[i].onClick.RemoveAllListeners();
                    stageButtons[i].onClick.AddListener(() => StartStage(stageNum));
                    stageButtons[i].GetComponentInChildren<TMP_Text>().text = $"Stage {stageNum}";

                    if (stageNum - 1 <= clearedStage)
                    {
                        stageButtons[i].interactable = true;
                    }
                    else
                    {
                        stageButtons[i].interactable = false;
                    }
                }
                else
                {
                    stageButtons[i].gameObject.SetActive(false);
                }
            }
        }

        private void StartStage(int stageNum)
        {
            cameraController.ResetPositionImmediately();
            cubeParent.gameObject.SetActive(true);
            exerciseDimension = CustomPlayerPrefs.GetInt(ENUM_PLAYERPREFS.ExerciseDimension);
            nowStage = stageNum;

            if (stageNum == 0 || stageNum > stageData.Length)
            {
                CreateCube();
            }
            else
            {
                var stage = stageData[stageNum - 1];
                if (stage == null || stage.Layers.Count == 0)
                {
                    Debug.LogError($"Stage {stageNum} data is missing or invalid.");
                    return;
                }

                CreateCube(stage);
            }

            UIManager.Instance.SetStageUI(nowStage);
        }

        private void CreateCube(CubeStage stage = null)
        {
            int dimension;
            if (stage != null)
            {
                dimension = stage.Dimension;
            }
            else
            {
                dimension = exerciseDimension;
            }

            cubeObject = Instantiate(cubePrefab[dimension - 2], cubeParent);
            var cube = cubeObject.GetComponent<Cube>();
            BlockWatcher.Instance.Initialize(cube);

            seeThroughButton.onClick.RemoveAllListeners();
            if (dimension == 2)
            {
                seeThroughButton.gameObject.SetActive(false);
            }
            else if (dimension == 3)
            {
                seeThroughButton.gameObject.SetActive(true);
                seeThroughButton.onClick.AddListener(cube.Set333CubeAlpha);
            }
            else if (dimension == 4)
            {
                seeThroughButton.gameObject.SetActive(true);
                seeThroughButton.onClick.AddListener(cube.Set444CubeAlpha);
            }

            cube.InitializeCube(stage);
            cameraController.SetCameraDistance(dimension);
        }

        private void RevealHintBlocks()
        {
            if (cubeObject == null)
                return;

            var cube = cubeObject.GetComponent<Cube>();
            cube.RevealHintBlocks();
        }

        public void StageClear()
        {
            if (nowStage > clearedStage)
            {
                stageButtons[nowStage % stageButtons.Length].interactable = true;
                CustomPlayerPrefs.SetInt(ENUM_PLAYERPREFS.ClearedStage, ++clearedStage);
            }
        }

        public void NextStage()
        {
            EndStage();
            StartStage(nowStage + 1);
        }

        public void RestartStage()
        {
            EndStage();
            StartStage(nowStage);
        }

        public void EscapeStage()
        {
            EndStage();

            if (nowStage == 0)
            {
                UIManager.Instance.GoTitle();
            }
            else
            {
                UIManager.Instance.EscapeStage();
            }
        }

        private void EndStage()
        {
            cubeParent.gameObject.SetActive(false);
            Destroy(cubeObject);
        }

#if UNITY_EDITOR
        [Button]
        private void ExportStagesAsJson()
        {
            stageData = CubeStage.FromJson(stageJson.text);

            for (int i = 0; i < stageData.Length; i++)
            {
                stageData[i].Id = i + 1;

                // 기본 색이 지정되지 않은 경우 아래 코드 사용
                // 현재 json 포맷에서는 position은 빈 블록 없이 모든 색에 대한 값을 갖고 있어야 한다.
                //foreach (var layer in stageData[i].Layers)
                //{
                //    int[] memo = new int[stageData[i].Dimension * stageData[i].Dimension];
                //    List<int> unsetPositions = new();
                //    List<ENUM_COLOR> colors = new(System.Enum.GetValues(typeof(ENUM_COLOR)).Cast<ENUM_COLOR>());
                //    foreach (var arrangement in layer.Arrangements)
                //    {
                //        foreach (var position in arrangement.Positions)
                //        {
                //            memo[position - 1] = 1;
                //        }
                //        colors.Remove(arrangement.Color);
                //    }

                //    for (int j = 0; j < memo.Length; j++)
                //    {
                //        if (memo[j] == 0)
                //            unsetPositions.Add(j + 1);
                //    }

                //    if (unsetPositions.Count > 0)
                //    {
                //        int ran = Random.Range(0, colors.Count);
                //        layer.Arrangements.Add(new Arrangement()
                //        {
                //            Color = colors[ran],
                //            Positions = unsetPositions.ToArray()
                //        });
                //    }
                //}
            }

            var stageStr = JsonConvert.SerializeObject(stageData, Formatting.Indented);
            File.WriteAllText(Path.Combine(Application.dataPath, "Resources/stage_data.json"), stageStr);
        }

        [Button]
        private void ValidateStages()
        {
            stageData = CubeStage.FromJson(stageJson.text);

            for (int i = 0; i < stageData.Length; i++)
            {
                if (!stageData[i].IsClearable())
                {
                    Debug.Log($"Stage {i + 1} is not clearable.");
                }
            }
        }

        [Button]
        private void FixStage(int id)
        {
            stageData = CubeStage.FromJson(stageJson.text);

            // id를 0으로 할 경우 전체 스테이지에 대해 검증
            if (id == 0)
            {
                foreach (var stage in stageData)
                {
                    if (!stage.IsClearable())
                    {
                        Debug.Log($"Stage {stage.Id} is not clearable. Try fix..");
                        if (!stage.Fix())
                        {
                            Debug.Log($"Failed to fix Stage {stage.Id}.");
                        }
                    }
                    else
                    {
                        Debug.Log($"Stage {stage.Id} is clearable.");
                    }
                }
            }
            else
            {
                if (!stageData[id - 1].IsClearable())
                {
                    if (!stageData[id - 1].Fix())
                    {
                        Debug.Log($"Failed to fix Stage {id}.");
                    }
                }
                else
                {
                    Debug.Log($"Stage {id} is clearable.");
                }
            }

            var stageStr = JsonConvert.SerializeObject(stageData, Formatting.Indented);
            File.WriteAllText(Path.Combine(Application.dataPath, "Resources/stage_data.json"), stageStr);
        }
#endif
    }
}
