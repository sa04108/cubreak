using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Vector2 startPos;
    Vector2 endPos;

    [HideInInspector]
    public List<GameObject> blocks;

    protected virtual void Awake()
    {
        startPos = Vector2.zero;
        endPos = Vector2.zero;
    }

    private void Update()
    {
        if (MouseClick() && BlockGroupStatus.Instance.FallingBlockCount == 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100.0f, 1 << 6))
                hit.transform.GetComponent<Block>().DestroyBlocks();
        }
    }

    private bool MouseClick()
    {
        if (Input.GetMouseButtonDown(0))
            startPos = Input.mousePosition;

        if (Input.GetMouseButton(0))
            endPos = Input.mousePosition;

        if (Input.GetMouseButtonUp(0))
        {
            if (Vector2.Distance(startPos, endPos) < Screen.width * 0.1f)
                return true;
        }

        return false;
        
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
            gameObject.SetActive(false);
        }
    }

    private void GameOverCheck()
    {
        if (BlockGroupStatus.Instance.BlockCount > 0
            && BlockGroupStatus.Instance.BlockCount == BlockGroupStatus.Instance.UnconnectedBlockCount
            && BlockGroupStatus.Instance.FallingBlockCount == 0)
        {
            UIManager.Instance.GameOver();
            gameObject.SetActive(false);
        }
    }
}
