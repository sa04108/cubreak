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

    private Vector3[] rayCastVec;
    public Vector3[] RayCastVec { get { return rayCastVec; } }

    private int score;
    [Header("In Game Variables")]
    public Text scoreText;
    public Text finalScoreText;

    private GameObject mainGameObjectsTemp;
    public GameObject mainGameObjects;

    private int numOfBlockColor;
    public int NumOfBlockColor { get { return numOfBlockColor; } }
    public Text numOfBlockColorText;
    
    [SerializeField]
    private int blockCount;
    public int BlockCount { get { return blockCount; } set { blockCount = value; } }

    [SerializeField]
    private int fallingBlockCount;
    public int FallingBlockCount { get { return fallingBlockCount; } set { fallingBlockCount = value; } }

    [SerializeField]
    private int unconnectedBlockCount;
    public int UnconnectedBlockCount { get { return unconnectedBlockCount; } set { unconnectedBlockCount = value; } }

    private void Awake()
    {
        numOfBlockColor = 4;
        rayCastVec = new Vector3[6];
        rayCastVec[0] = Vector3.forward;
        rayCastVec[1] = Vector3.back;
        rayCastVec[2] = Vector3.left;
        rayCastVec[3] = Vector3.right;
        rayCastVec[4] = Vector3.up;
        rayCastVec[5] = Vector3.down;
    }

    // Start is called before the first frame update
    void Start()
    {
        numOfBlockColorText.text = numOfBlockColor.ToString();
        Init();
    }

    private void Update()
    {

    }

    private void Init()
    {
        blockCount = 0;
        unconnectedBlockCount = 0;
        fallingBlockCount = 0;
        score = 0;
        scoreText.text = "0";
    }

    public void SetNumberOfBlockColor(int num)
    {
        numOfBlockColor += num;
        if (numOfBlockColor < 1) numOfBlockColor = 1;
        else if (numOfBlockColor > 6) numOfBlockColor = 6;
        numOfBlockColorText.text = numOfBlockColor.ToString();
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
        if (blockCount > 0 && blockCount == unconnectedBlockCount && fallingBlockCount == 0)
        {
            gameOverPanel.SetActive(true);
            finalScoreText.text = scoreText.text;
        }
    }
}
