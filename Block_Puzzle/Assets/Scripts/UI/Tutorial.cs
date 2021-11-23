using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    int sceneNum;
    public RectTransform panel;
    public Text tutorialText;

    private void Awake()
    {
        sceneNum = 0;
    }

    public void NextStep()
    {
        switch (++sceneNum)
        {
            case 1:
                tutorialText.text = "같은 색으로 연결되어있는 블록들만 파괴됩니다!";
                break;
            case 2:
                panel.sizeDelta = Vector2.up * 2000f;
                panel.anchorMin = Vector2.up;
                panel.anchorMax = Vector2.one;
                panel.pivot = new Vector2(0.5f, 1.0f);
                tutorialText.text = "Left와 Right 버튼으로 큐브를 좌우로 회전시킬 수 있습니다.";
                break;
            case 3:
                panel.sizeDelta = Vector2.up * 1200f;
                panel.anchorMin = Vector2.zero;
                panel.anchorMax = Vector2.right;
                panel.pivot = new Vector2(0.5f, 0.0f);
                tutorialText.text = "스테이지 게임에서는 모든 블록을 파괴해야 클리어할 수 있습니다.";
                break;
            case 4:
                tutorialText.text = "연습 게임에서는 최대한 많은 블록을 파괴하면 됩니다!";
                break;            
            case 5:
                tutorialText.text = "시간 제한은 없습니다!\n\n당신의 공간지각능력을 충분히 발휘해보세요!";
                break;
            default:
                gameObject.SetActive(false);
                break;
        }
    }
}
