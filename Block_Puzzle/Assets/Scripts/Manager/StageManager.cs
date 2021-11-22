using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private int nowStage;

    public BlockGenerator blockGenerator;
    public GameObject cube222;
    public GameObject cube333;

    public void StartStage(int stageNum)
    {
        nowStage = stageNum;

        if (stageNum == 0)
        {
            RandomizedGameManager.Instance.gameObject.SetActive(true);
            blockGenerator.CreateCubeAndPattern(cube333, stageNum);
        }
        else
        {
            UIManager.Instance.SetActivePanel(UIManager.Instance.inGamePanel);
            PatternedGameManager.Instance.gameObject.SetActive(true);

            switch (stageNum)
            {
                case 0:
                    blockGenerator.CreateCubeAndPattern(cube333, stageNum);
                    break;
                case 1:
                case 2:
                    blockGenerator.CreateCubeAndPattern(cube222, stageNum);
                    break;
                case 3:
                case 4:
                    blockGenerator.CreateCubeAndPattern(cube333, stageNum);
                    break;
                default:
                    break;
            }
        }
    }

    public void RestartStage()
    {
        blockGenerator.DestroyBlocks();
        UIManager.Instance.RestartGame();
        StartStage(nowStage);
    }

    public void EndStage()
    {
        blockGenerator.DestroyBlocks();
        UIManager.Instance.GoTitle();
        PatternedGameManager.Instance.gameObject.SetActive(false);
        RandomizedGameManager.Instance.gameObject.SetActive(false);
    }
}
