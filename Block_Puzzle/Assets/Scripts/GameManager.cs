using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject titlePanel;
    public GameObject mainPanel;
    public GameObject gameOverPanel;
    public GameObject devOptionPanel;

    private int score;
    [Header("Score Variables")]
    public Text scoreText;
    public Text finalScoreText;

    private GameObject mainGameObjectsTemp;
    [Header("Main Game Object Prefab")]
    public GameObject mainGameObjects;

    [Header("Number of Block Color Text")]
    public Text numOfBlockColorText;

    private BlockGroupStatus blockGroupStatus;

    private void Awake()
    {
        blockGroupStatus = FindObjectOfType<BlockGroupStatus>();
        numOfBlockColorText.text = blockGroupStatus.NumOfBlockColor.ToString();
        Init();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {

    }

    private void Init()
    {
        score = 0;
        scoreText.text = "0";

        blockGroupStatus.NumOfBlockColor = 4;
        blockGroupStatus.BlockCount = 0;
        blockGroupStatus.FallingBlockCount = 0;
        blockGroupStatus.UnconnectedBlockCount = 0;
    }

    public void SetNumberOfBlockColor(int num)
    {
        blockGroupStatus.NumOfBlockColor += num;
        numOfBlockColorText.text = blockGroupStatus.NumOfBlockColor.ToString();
    }

    public void OnOffDevOptionPanel()
    {
        devOptionPanel.SetActive(!devOptionPanel.activeSelf);
    }

    public void StartGame()
    {
        titlePanel.SetActive(false);
        mainPanel.SetActive(true);
        mainGameObjectsTemp = Instantiate(mainGameObjects);
    }

    public void RestartGame()
    {
        gameOverPanel.SetActive(false);
        Init();
        Destroy(mainGameObjectsTemp);
        mainGameObjectsTemp = Instantiate(mainGameObjects);
    }

    public void GoTitle()
    {
        gameOverPanel.SetActive(false);
        Init();
        Destroy(mainGameObjectsTemp);
        mainPanel.SetActive(false);
        titlePanel.SetActive(true);
    }

    public void ScoreUp()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void GameOverCheck()
    {
        if (blockGroupStatus.BlockCount > 0
            && blockGroupStatus.BlockCount == blockGroupStatus.UnconnectedBlockCount
            && blockGroupStatus.FallingBlockCount == 0)
        {
            gameOverPanel.SetActive(true);
            finalScoreText.text = scoreText.text;
        }
    }
}
