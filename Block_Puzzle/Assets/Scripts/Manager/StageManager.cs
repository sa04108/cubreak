using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    private int nowStage;

    public BlockGenerator blockGenerator;
    public GameObject cube222;
    public GameObject cube333;

    public void StartStage(int stageNum)
    {
        nowStage = stageNum;

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

    public void NextStage()
    {
        if (nowStage <= 3)
        {
            blockGenerator.DestroyBlocks();
            UIManager.Instance.StartStage(++nowStage);
        }
        else
            EndStage();
    }

    public void RestartStage()
    {
        blockGenerator.DestroyBlocks();
        UIManager.Instance.StartStage(nowStage);
    }

    public void EndStage()
    {
        blockGenerator.DestroyBlocks();
        UIManager.Instance.GoTitle();
    }
}
