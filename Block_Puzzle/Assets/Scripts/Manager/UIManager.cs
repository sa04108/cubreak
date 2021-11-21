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
    public GameObject devOptionPanel;
    public GameObject inGamePanel;
    public GameObject gameClearPanel;
    public GameObject gameOverPanel;

    [Header("Randomized Game Objects")]
    public GameObject RGameObjects;

    [Header("Patterned Game Objects")]
    public GameObject PGameObjects;

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

    private bool onPatternGame;
    public bool OnPatternGame { get => onPatternGame; }

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

    private void Init()
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
    public void OnOffDevOptionPanel()
    {
        devOptionPanel.SetActive(!devOptionPanel.activeSelf);
    }

    public void StartRandomizedGame()
    {
        onPatternGame = false;
        titlePanel.SetActive(false);
        inGamePanel.SetActive(true);
        RGameObjects.SetActive(true);
    }

    public void StartPatternedGame()
    {
        onPatternGame = true;
        titlePanel.SetActive(false);
        inGamePanel.SetActive(true);
        PGameObjects.SetActive(true);
    }

    public void RestartGame()
    {
        gameClearPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        Init();

        if (RGameObjects.activeSelf)
        {
            RGameObjects.SetActive(false);
            RGameObjects.SetActive(true);
        }
        else if (PGameObjects.activeSelf)
        {
            PGameObjects.SetActive(false);
            PGameObjects.SetActive(true);
        }
    }

    public void GoTitle()
    {
        gameClearPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        Init();
        RGameObjects.SetActive(false);
        PGameObjects.SetActive(false);
        inGamePanel.SetActive(false);
        titlePanel.SetActive(true);
    }

    public void GameClear()
    {
        gameClearPanel.SetActive(true);
        finalScoreText.text = scoreText.text;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        finalScoreText.text = scoreText.text;
    }
    #endregion
}
