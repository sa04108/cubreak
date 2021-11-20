using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Number of Block Colors Text")]
    public Text numOfBlockColorText;

    [Header("Block Falling Speed Text")]
    public Text blockFallingSpeedText;

    private BlockGenerator blockGenerator;
    private BlockGroupStatus blockGroupStatus;

    private void Awake()
    {
        blockGroupStatus = FindObjectOfType<BlockGroupStatus>();

        blockGroupStatus.NumOfBlockColor = 4;
        numOfBlockColorText.text = blockGroupStatus.NumOfBlockColor.ToString();
        blockGroupStatus.BlockFallingSpeed = 3.0f;
        blockFallingSpeedText.text = blockGroupStatus.BlockFallingSpeed.ToString();

        Init();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void LateUpdate()
    {
        GenerateBlocks();
        GameOverCheck();
    }

    private void Init()
    {
        score = 0;
        scoreText.text = "0";

        blockGroupStatus.BlockCount = 0;
        blockGroupStatus.FallingBlockCount = 0;
        blockGroupStatus.UnconnectedBlockCount = 0;
    }

    private void GenerateBlocks()
    {
        if (blockGroupStatus.FallingBlockCount == 0 && blockGenerator != null)
            blockGenerator.GenerateBlocks();
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

    public void OnOffDevOptionPanel()
    {
        devOptionPanel.SetActive(!devOptionPanel.activeSelf);
    }

    public void StartGame()
    {
        titlePanel.SetActive(false);
        mainPanel.SetActive(true);
        mainGameObjectsTemp = Instantiate(mainGameObjects);
        blockGenerator = mainGameObjectsTemp.GetComponentInChildren<BlockGenerator>();
    }

    public void RestartGame()
    {
        gameOverPanel.SetActive(false);
        Init();
        Destroy(mainGameObjectsTemp);
        mainGameObjectsTemp = Instantiate(mainGameObjects);
        blockGenerator = mainGameObjectsTemp.GetComponentInChildren<BlockGenerator>();
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
