using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private BlockGroupStatus blockGroupStatus;

    [Header("Panels")]
    public GameObject titlePanel;
    public GameObject mainPanel;
    public GameObject gameOverPanel;
    public GameObject devOptionPanel;

    [Header("Main Game Objects")]
    public GameObject mainGameObjects;

    private int score;
    [Header("Score Variables")]
    public Text scoreText;
    public Text finalScoreText;

    [Header("Number of Block Colors Text")]
    public Text numOfBlockColorText;

    [Header("Block Falling Speed Text")]
    public Text blockFallingSpeedText;

    private void Awake()
    {
        blockGroupStatus = FindObjectOfType<BlockGroupStatus>();

        Init();
    }

    private void Init()
    {
        score = 0;
        scoreText.text = "0";

        blockGroupStatus.BlockCount = 0;
        blockGroupStatus.FallingBlockCount = 0;
        blockGroupStatus.UnconnectedBlockCount = 0;
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

    public void OnOffDevOptionPanel()
    {
        devOptionPanel.SetActive(!devOptionPanel.activeSelf);
    }

    public void StartGame()
    {
        titlePanel.SetActive(false);
        mainPanel.SetActive(true);
        mainGameObjects.SetActive(true);
    }

    public void RestartGame()
    {
        gameOverPanel.SetActive(false);
        Init();
        mainGameObjects.SetActive(false);
        mainGameObjects.SetActive(true);
    }

    public void GoTitle()
    {
        gameOverPanel.SetActive(false);
        Init();
        mainGameObjects.SetActive(false);
        mainPanel.SetActive(false);
        titlePanel.SetActive(true);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        finalScoreText.text = scoreText.text;
    }
}
