using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cublocks
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
        [SerializeField] private CameraPos cameraPos;
        [SerializeField] private Transform cubeParent;
        [SerializeField] private GameObject[] cubePrefab;
        [SerializeField] private Button[] stageButtons;
        [SerializeField] private Button exerciseButton;
        [SerializeField] private Button prevButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button seeThroughButton;

        private void Start()
        {
            stageData = CubeStage.FromJson(stageJson.text);
            clearedStage = CustomPlayerPrefs.GetInt(ENUM_PLAYERPREFS.ClearedStage, 0);
            stageGroupIndex = clearedStage / stageButtons.Length;

            InitializeStagePanel();

            exerciseButton.onClick.AddListener(() => StartStage(0));

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
            BlockWatcher.Instance?.Initialize();
            exerciseDimension = CustomPlayerPrefs.GetInt(ENUM_PLAYERPREFS.ExerciseDimension);
            nowStage = stageNum;

            if (stageNum == 0)
            {
                CreateExerciseCube();
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

        private void CreateCube(CubeStage stage)
        {
            cubeObject = Instantiate(cubePrefab[stage.Dimension - 2], cubeParent);
            var cubeMat = cubeObject.GetComponent<CubeMaterial>();

            seeThroughButton.onClick.RemoveAllListeners();
            if (stage.Dimension == 3)
            {
                seeThroughButton.onClick.AddListener(cubeMat.Set333CubeAlpha);
            }
            else if (stage.Dimension == 4)
            {
                seeThroughButton.onClick.AddListener(cubeMat.Set444CubeAlpha);
            }

            foreach (var layer in stage.Layers)
            {
                foreach (var arrangement in layer.Arrangements)
                {
                    cubeMat.SetFloorColor(layer.Index, arrangement.Positions, arrangement.Color);
                }
            }

            cameraPos.SetCameraDistance(stage.Dimension - 2);
        }

        private void CreateExerciseCube()
        {
            cubeObject = Instantiate(cubePrefab[exerciseDimension - 2], cubeParent);
            var cubeMat = cubeObject.GetComponent<CubeMaterial>();
            var cubeBlocks = cubeObject.GetComponent<CubeBlocks>();

            seeThroughButton.onClick.RemoveAllListeners();
            if (exerciseDimension == 3)
            {
                seeThroughButton.onClick.AddListener(cubeMat.Set333CubeAlpha);
            }
            else if (exerciseDimension == 4)
            {
                seeThroughButton.onClick.AddListener(cubeMat.Set444CubeAlpha);
            }

            foreach (var floor in cubeBlocks.floors)
            {
                foreach (var block in floor.floor)
                {
                    block.GetComponent<Block>().SetColor(null);
                }
            }

            cameraPos.SetCameraDistance(exerciseDimension - 2);
        }

        private void EndStage()
        {
            Destroy(cubeObject);
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
            UIManager.Instance.SetStageUI(nowStage + 1);
        }

        public void RestartStage()
        {
            EndStage();
            StartStage(nowStage);
            UIManager.Instance.SetStageUI(nowStage);
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
    } 
}
