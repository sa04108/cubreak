using TMPro;
using UnityEngine;
using UnityEngine.UI;

enum CubeModel
{
    cube222,
    cube333,
    cube444
}

public class StageManager : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject cubeObject;

    private int nowStage;
    private int clearedStage;
    private int stageGroupIndex; // start with 0
    private int dimensionIdx;
    public int DimensionIdx
    {
        get => dimensionIdx;
        set
        {
            dimensionIdx = value;
            if (dimensionIdx < 0) dimensionIdx = 0;
            else if (dimensionIdx > 2) dimensionIdx = 2;
        }
    }

    private CubeStage[] stageData;

    [SerializeField] TextAsset stageJson;
    [SerializeField] CameraPos cameraPos;
    [SerializeField] GameObject[] cubePrefab;
    [SerializeField] Button[] stageButtons;
    [SerializeField] Button prevButton;
    [SerializeField] Button nextButton;

    private void Start()
    {
        gameManager = GameManager.Instance;

        stageData = CubeStage.FromJson(stageJson.text);
        clearedStage = PlayerPrefs.GetInt("ClearedStage", 0);
        stageGroupIndex = clearedStage / stageButtons.Length;

        InitializeStagePanel();

        prevButton.onClick.AddListener((UnityEngine.Events.UnityAction)(() =>
        {
            stageGroupIndex--;

            if (stageGroupIndex < 0)
            {
                stageGroupIndex = 0;
                return;
            }

            InitializeStagePanel();
        }));

        nextButton.onClick.AddListener((UnityEngine.Events.UnityAction)(() =>
        {
            stageGroupIndex++;

            int maxStageNum = stageData.Length;
            if (stageGroupIndex > (maxStageNum - 1) / stageButtons.Length)
            {
                stageGroupIndex--;
                return;
            }

            InitializeStagePanel();
        }));
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

    private void CreateCubeAndPattern(int modelIdx, CubeStage stage, ENUM_BLOCK_TYPE stageType = ENUM_BLOCK_TYPE.PATTERNED)
    {
        gameManager.blockType = stageType;
        gameManager.gameObject.SetActive(true);
        cubeObject = Instantiate(cubePrefab[modelIdx], transform.position, Quaternion.identity, transform.parent);

        GenerateCubeStage(stage);

        cameraPos.SetCameraDistance(modelIdx);
    }

    private void GenerateCubeStage(CubeStage stage)
    {
        foreach (var layer in stage.Layers)
        {
            foreach (var arrangement in layer.Arrangements)
            {
                cubeObject.GetComponent<CubeMaterial>().SetFloorColor(layer.Index, arrangement.Positions, arrangement.Color);
            }
        }
    }

    private void DestroyBlocks()
    {
        Destroy(cubeObject);
        gameManager.gameObject.SetActive(false);
    }

    private void StartStage(int stageNum)
    {
        nowStage = stageNum;

        if (stageNum == 0)
        {
            CreateCubeAndPattern(dimensionIdx, stageData[stageNum]);
        }
        else
        {
            var stage = stageData[stageNum - 1];
            if (stage == null || stage.Layers.Count == 0)
            {
                Debug.LogError($"Stage {stageNum} data is missing or invalid.");
                return;
            }

            CreateCubeAndPattern(stage.Dimension - 2, stage);
        }

        UIManager.Instance.SetStageUI(nowStage);
    }

    public void StageClear()
    {
        if (nowStage > clearedStage)
        {
            stageButtons[nowStage % stageButtons.Length].interactable = true;
            PlayerPrefs.SetInt("ClearedStage", ++clearedStage);
        }
    }

    public void NextStage()
    {
        DestroyBlocks();
        StartStage(nowStage + 1);
        UIManager.Instance.SetStageUI(nowStage + 1);
    }

    public void RestartStage()
    {
        DestroyBlocks();
        StartStage(nowStage);
        UIManager.Instance.SetStageUI(nowStage);
    }

    public void EndStage()
    {
        DestroyBlocks();
        UIManager.Instance.GoTitle();
    }
}
