using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private BlockGroupStatus blockGroupStatus;

    [Header("Panels")]
    [SerializeField] private GameObject titlePanel;
    [SerializeField] private GameObject stagesPanel;
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject gameClearPanel;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Title Buttons")]
    [SerializeField] private Button selectStageButton;
    [SerializeField] private Button optionButton;

    [Header("Stage Manager")]
    [SerializeField] private StageManager stageManager;
    [SerializeField] private TMP_Text stageText;
    [SerializeField] private Button nextStageButton;

    private int score;
    [Header("Score Variables")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text finalScoreText;

    [Header("Block Falling Speed Text")]
    [SerializeField] private TMP_Text blockFallingSpeedText;

    [Header("Cube Model Text")]
    [SerializeField] private TMP_Text cubeModelText;

    [Header("Number of Block Colors Text")]
    [SerializeField] private TMP_Text numOfBlockColorText;

    private void Start()
    {
        blockGroupStatus = BlockGroupStatus.Instance;
        blockGroupStatus.BlockFallingSpeed = 3.0f;
        blockFallingSpeedText.text = blockGroupStatus.BlockFallingSpeed.ToString();
        blockGroupStatus.NumOfBlockColor = 4;
        numOfBlockColorText.text = blockGroupStatus.NumOfBlockColor.ToString();
        stageManager.ExerciseDimension = (int)CubeModel.cube333 + 2;
        cubeModelText.text = "3x3x3";

        Init();
        SetActivePanel(titlePanel);

        selectStageButton.onClick.AddListener(() => SetActivePanel(stagesPanel));
        optionButton.onClick.AddListener(() => SetActivePanel(optionPanel));
    }

    public void Init()
    {
        score = 0;
        scoreText.text = "0";

        blockGroupStatus.BlockCount = 0;
        blockGroupStatus.FallingBlockCount = 0;
        blockGroupStatus.UnconnectedBlockCount = 0;
    }

    public void SetBlockFallingSpeed(float num)
    {
        blockGroupStatus.BlockFallingSpeed += num;
        blockFallingSpeedText.text = blockGroupStatus.BlockFallingSpeed.ToString();
    }

    public void SetCubeModel(int num)
    {
        stageManager.ExerciseDimension += num;
        cubeModelText.text = stageManager.ExerciseDimension + "x" + stageManager.ExerciseDimension + "x" + stageManager.ExerciseDimension;
    }

    public void SetNumberOfBlockColor(int num)
    {
        blockGroupStatus.NumOfBlockColor += num;
        numOfBlockColorText.text = blockGroupStatus.NumOfBlockColor.ToString();
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        SetActivePanel(titlePanel);
    }

    public void ScoreUp()
    {
        score++;
        scoreText.text = score.ToString();
    }

    #region Panel Management
    public void SetActivePanel(GameObject panel)
    {
        titlePanel.SetActive(panel.name == titlePanel.name);
        stagesPanel.SetActive(panel.name == stagesPanel.name);
        optionPanel.SetActive(panel.name == optionPanel.name);
        inGamePanel.SetActive(panel.name == inGamePanel.name);
        gameClearPanel.SetActive(panel.name == gameClearPanel.name);
        gameOverPanel.SetActive(panel.name == gameOverPanel.name);
    }

    public void SetStageUI(int stageNum)
    {
        Init();
        SetActivePanel(inGamePanel);
        if (stageNum == 0)
        {
            scoreText.gameObject.SetActive(true);
            stageText.gameObject.SetActive(false);
            nextStageButton.gameObject.SetActive(false);
        }
        else
        {
            scoreText.gameObject.SetActive(false);
            stageText.gameObject.SetActive(true);
            stageText.text = "Stage " + stageNum;
            nextStageButton.gameObject.SetActive(true);
        }
    }

    public void GoTitle()
    {
        Init();
        SetActivePanel(titlePanel);
    }

    public void GameClear()
    {
        SetActivePanel(gameClearPanel);
        finalScoreText.text = scoreText.text;
        stageManager.StageClear();
    }

    public void GameOver()
    {
        SetActivePanel(gameOverPanel);
        finalScoreText.text = scoreText.text;
    }
    #endregion
}
