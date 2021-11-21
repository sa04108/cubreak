using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> blocks;

    private BlockGroupStatus blockGroupStatus;
    private UIManager uiManager;

    protected virtual void Awake()
    {
        uiManager = UIManager.Instance;
        blockGroupStatus = BlockGroupStatus.Instance;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && blockGroupStatus.FallingBlockCount == 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Block"))
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
        if (blockGroupStatus.BlockCount == 0
            && blockGroupStatus.UnconnectedBlockCount == 0
            && blockGroupStatus.FallingBlockCount == 0)
        {
            uiManager.GameClear();
        }
    }

    private void GameOverCheck()
    {
        if (blockGroupStatus.BlockCount > 0
            && blockGroupStatus.BlockCount == blockGroupStatus.UnconnectedBlockCount
            && blockGroupStatus.FallingBlockCount == 0)
        {
            uiManager.GameOver();
        }
    }
}
