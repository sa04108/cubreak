using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> blocks;

    protected virtual void Awake() { }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && BlockGroupStatus.Instance.FallingBlockCount == 0)
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
