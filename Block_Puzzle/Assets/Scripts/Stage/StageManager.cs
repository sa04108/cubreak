using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum CubeModel {
    cube222,
    cube333,
    cube444
}

public class StageManager : MonoBehaviour {
    private int nowStage;
    private int clearedStage;

    private int modelIdx;
    public int ModelIdx {
        get => modelIdx;
        set {
            modelIdx = value;
            if (modelIdx < 0) modelIdx = 0;
            else if (modelIdx > 2) modelIdx = 2;
        }
    }

    [SerializeField] GameDirector gameDirector;
    [SerializeField] Button[] stageButtons;

    private void Awake() {
        clearedStage = PlayerPrefs.GetInt("ClearedStage", 0);
        for (int i = 0; i <= clearedStage; i++) {
            stageButtons[i].interactable = true;
        }
    }

    public void StartStage(int stageNum) {
        nowStage = stageNum;

        switch (stageNum) {
            // stage 0 is random pattern game
            case 0:
                gameDirector.CreateCubeAndPattern(modelIdx, stageNum);
                break;
            case 1:
            case 2:
                gameDirector.CreateCubeAndPattern((int)CubeModel.cube222, stageNum);
                break;
            case 3:
            case 4:
            case 5:
            case 6:
                gameDirector.CreateCubeAndPattern((int)CubeModel.cube333, stageNum);
                break;
            default:
                break;
        }
    }

    public void NextStage() {
        gameDirector.DestroyBlocks();
        UIManager.Instance.StartStage(nowStage + 1);
    }

    public void SetStageUp() {
        if (nowStage > clearedStage && stageButtons.Length > nowStage) {
            stageButtons[nowStage].interactable = true;
            PlayerPrefs.SetInt("ClearedStage", ++clearedStage);
        }
    }

    public void RestartStage() {
        gameDirector.DestroyBlocks();
        UIManager.Instance.StartStage(nowStage);
    }

    public void EndStage() {
        gameDirector.DestroyBlocks();
        UIManager.Instance.GoTitle();
    }
}
