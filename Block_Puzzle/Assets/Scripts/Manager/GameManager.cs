using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    InputManager inputManager;
    delegate bool InputType();
    InputType click;
    InputType slide;

    [HideInInspector] public List<GameObject> blocks;
    public ENUM_BLOCK_TYPE blockType = ENUM_BLOCK_TYPE.UNDEFINED;

    private new void Awake()
    {
        base.Awake();
        inputManager = InputManager.Instance;
        click = new InputType(inputManager.Click);
        slide = new InputType(inputManager.Slide);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        slide();
        if (click() && BlockGroupStatus.Instance.FallingBlockCount == 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100.0f, 1 << 6)) {
                hit.transform.GetComponent<Block>().DestroyBlocks();
            }
        }
    }

    private void LateUpdate()
    {
        BlocksMoveDownCheck();
        BlocksCheckmateCheck();
        GameClearCheck();
        GameOverCheck();
    }

    private void BlocksMoveDownCheck()
    {
        for (int i = 0; i < blocks.Count; i++)
        {
            if (blocks[i] != null)
            {
                if (!blocks[i].GetComponent<Block>().IsFalling)
                    StartCoroutine(blocks[i].GetComponent<Block>().MoveDown());
            }
            else
                blocks.RemoveAt(i--);
        }
    }

    private void BlocksCheckmateCheck()
    {
        for (int i = 0; i < blocks.Count; i++)
        {
            if (blocks[i] != null)
            {
                if (!blocks[i].GetComponent<Block>().IsFalling)
                    blocks[i].GetComponent<Block>().CheckmateCheck();
            }
            else
                blocks.RemoveAt(i--);
        }
    }

    private void GameClearCheck()
    {
        if (BlockGroupStatus.Instance.BlockCount == 0
            && BlockGroupStatus.Instance.UnconnectedBlockCount == 0
            && BlockGroupStatus.Instance.FallingBlockCount == 0)
        {
            UIManager.Instance.GameClear();
        }
    }

    private void GameOverCheck()
    {
        if (BlockGroupStatus.Instance.BlockCount > 0
            && BlockGroupStatus.Instance.BlockCount == BlockGroupStatus.Instance.UnconnectedBlockCount
            && BlockGroupStatus.Instance.FallingBlockCount == 0)
        {
            UIManager.Instance.GameOver();
        }
    }
}
