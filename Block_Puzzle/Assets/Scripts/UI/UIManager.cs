using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {
    private BlockGroupStatus blockGroupStatus;

    [Header("Panels")]
    public GameObject titlePanel;
    public GameObject stagesPanel;
    public GameObject optionPanel;
    public GameObject inGamePanel;
    public GameObject gameClearPanel;
    public GameObject gameOverPanel;

    [Header("Stage Manager")]
    public StageManager stageManager;
    public Text stageText;
    public Button nextStageButton;

    private int score;
    [Header("Score Variables")]
    public Text scoreText;
    public Text finalScoreText;

    [Header("Block Falling Speed Text")]
    public Text blockFallingSpeedText;

    [Header("Cube Model Text")]
    public Text cubeModelText;

    [Header("Number of Block Colors Text")]
    public Text numOfBlockColorText;

    private void Start() {
        blockGroupStatus = BlockGroupStatus.Instance;
        blockGroupStatus.BlockFallingSpeed = 3.0f;
        blockFallingSpeedText.text = blockGroupStatus.BlockFallingSpeed.ToString();
        blockGroupStatus.NumOfBlockColor = 4;
        numOfBlockColorText.text = blockGroupStatus.NumOfBlockColor.ToString();
        stageManager.ModelIdx = (int)CubeModel.cube333;
        cubeModelText.text = "3x3x3";

        Init();
        SetActivePanel(titlePanel);
    }

    public void Init() {
        score = 0;
        scoreText.text = "0";

        blockGroupStatus.BlockCount = 0;
        blockGroupStatus.FallingBlockCount = 0;
        blockGroupStatus.UnconnectedBlockCount = 0;
    }

    public void SetBlockFallingSpeed(float num) {
        blockGroupStatus.BlockFallingSpeed += num;
        blockFallingSpeedText.text = blockGroupStatus.BlockFallingSpeed.ToString();
    }

    public void SetCubeModel(int num) {
        stageManager.ModelIdx += num;
        cubeModelText.text = (stageManager.ModelIdx + 2) + "x" + (stageManager.ModelIdx + 2) + "x" + (stageManager.ModelIdx + 2);
    }

    public void SetNumberOfBlockColor(int num) {
        blockGroupStatus.NumOfBlockColor += num;
        numOfBlockColorText.text = blockGroupStatus.NumOfBlockColor.ToString();
    }

    public void ResetGame() {
        PlayerPrefs.DeleteAll();
        SetActivePanel(titlePanel);
    }

    public void ScoreUp() {
        score++;
        scoreText.text = score.ToString();
    }

    #region Panel Management
    public void SetActivePanel(GameObject panel) {
        titlePanel.SetActive(panel.name == titlePanel.name);
        stagesPanel.SetActive(panel.name == stagesPanel.name);
        optionPanel.SetActive(panel.name == optionPanel.name);
        inGamePanel.SetActive(panel.name == inGamePanel.name);
        gameClearPanel.SetActive(panel.name == gameClearPanel.name);
        gameOverPanel.SetActive(panel.name == gameOverPanel.name);
    }

    public void StartStage(int stageNum) {
        Init();
        SetActivePanel(inGamePanel);
        if (stageNum == 0) {
            scoreText.gameObject.SetActive(true);
            stageText.gameObject.SetActive(false);
            nextStageButton.gameObject.SetActive(false);
        }
        else {
            scoreText.gameObject.SetActive(false);
            stageText.gameObject.SetActive(true);
            stageText.text = "Stage " + stageNum;
            nextStageButton.gameObject.SetActive(true);
        }
        stageManager.StartStage(stageNum);
    }

    public void GoTitle() {
        Init();
        SetActivePanel(titlePanel);
    }

    public void GameClear() {
        SetActivePanel(gameClearPanel);
        finalScoreText.text = scoreText.text;
        stageManager.SetStageUp();
    }

    public void GameOver() {
        SetActivePanel(gameOverPanel);
        finalScoreText.text = scoreText.text;
    }
    #endregion
}
