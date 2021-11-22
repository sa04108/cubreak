using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(UIManager)) as UIManager;

            return instance;
        }
    }

    private BlockGroupStatus blockGroupStatus;

    [Header("Panels")]
    public GameObject titlePanel;
    public GameObject stagesPanel;
    public GameObject devOptionPanel;
    public GameObject inGamePanel;
    public GameObject gameClearPanel;
    public GameObject gameOverPanel;

    [Header("Stage Manager")]
    public StageManager stageManager;
    public Text stageText;

    private int score;
    [Header("Score Variables")]
    public Text scoreText;
    public Text finalScoreText;

    [Header("Number of Block Colors Text")]
    public Text numOfBlockColorText;

    [Header("Block Falling Speed Text")]
    public Text blockFallingSpeedText;

    [Header("Camera Transform")]
    public Transform cameraTransform;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        blockGroupStatus = BlockGroupStatus.Instance;
        blockGroupStatus.NumOfBlockColor = 4;
        numOfBlockColorText.text = blockGroupStatus.NumOfBlockColor.ToString();
        blockGroupStatus.BlockFallingSpeed = 3.0f;
        blockFallingSpeedText.text = blockGroupStatus.BlockFallingSpeed.ToString();

        Init();
    }

    public void Init()
    {
        score = 0;
        scoreText.text = "0";

        blockGroupStatus.BlockCount = 0;
        blockGroupStatus.FallingBlockCount = 0;
        blockGroupStatus.UnconnectedBlockCount = 0;

        cameraTransform.localPosition = Vector3.zero;
    }

    public void SetNumberOfBlockColor(int num)
    {
        blockGroupStatus.NumOfBlockColor += num;
        numOfBlockColorText.text = blockGroupStatus.NumOfBlockColor.ToString();
    }

    public void SetBlockFallingSpeed(float num)
    {
        blockGroupStatus.BlockFallingSpeed += num;
        blockFallingSpeedText.text = blockGroupStatus.BlockFallingSpeed.ToString();
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
        devOptionPanel.SetActive(panel.name == devOptionPanel.name);
        inGamePanel.SetActive(panel.name == inGamePanel.name);
        gameClearPanel.SetActive(panel.name == gameClearPanel.name);
        gameOverPanel.SetActive(panel.name == gameOverPanel.name);
    }

    public void StartStage(int stageNum)
    {
        Init();
        SetActivePanel(inGamePanel);
        if (stageNum == 0)
        {
            scoreText.gameObject.SetActive(true);
            stageText.gameObject.SetActive(false);
            RandomizedGameManager.Instance.gameObject.SetActive(true);
        }
        else
        {
            scoreText.gameObject.SetActive(false);
            stageText.gameObject.SetActive(true);
            stageText.text = "Stage " + stageNum;
            PatternedGameManager.Instance.gameObject.SetActive(true);
        }

        stageManager.StartStage(stageNum);
    }

    public void GoTitle()
    {
        Init();
        SetActivePanel(titlePanel);
        PatternedGameManager.Instance.gameObject.SetActive(false);
        RandomizedGameManager.Instance.gameObject.SetActive(false);
    }

    public void GameClear()
    {
        SetActivePanel(gameClearPanel);
        finalScoreText.text = scoreText.text;
    }

    public void GameOver()
    {
        SetActivePanel(gameOverPanel);
        finalScoreText.text = scoreText.text;
    }
    #endregion
}
