using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum CubeModel
{
    cube222,
    cube333,
    cube444
}

public class StageManager : MonoBehaviour
{
    private int nowStage;

    private int modelIdx;
    public int ModelIdx
    {
        get => modelIdx;
        set
        {
            modelIdx = value;
            if (modelIdx < 0) modelIdx = 0;
            else if (modelIdx > 2) modelIdx = 2;
        }
    }

    public BlockGenerator blockGenerator;

    public void StartStage(int stageNum)
    {
        nowStage = stageNum;

        switch (stageNum)
        {
            // stage 0 is random pattern game
            case 0:
                blockGenerator.CreateCubeAndPattern(modelIdx, stageNum);
                break;
            case 1:
            case 2:
                blockGenerator.CreateCubeAndPattern((int)CubeModel.cube222, stageNum);
                break;
            case 3:
            case 4:
                blockGenerator.CreateCubeAndPattern((int)CubeModel.cube333, stageNum);
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
