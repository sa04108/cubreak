using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> blocks;

    private BlockGroupStatus blockGroupStatus;
    private UIManager uiManager;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        blockGroupStatus = FindObjectOfType<BlockGroupStatus>();

        blockGroupStatus.NumOfBlockColor = 4;
        uiManager.numOfBlockColorText.text = blockGroupStatus.NumOfBlockColor.ToString();
        blockGroupStatus.BlockFallingSpeed = 3.0f;
        uiManager.blockFallingSpeedText.text = blockGroupStatus.BlockFallingSpeed.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {

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
        GameOverCheck();
    }

    public void BlocksMoveDownCheck()
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

    public void BlocksCheckmateCheck()
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

    public void GameOverCheck()
    {
        if (blockGroupStatus.BlockCount > 0
            && blockGroupStatus.BlockCount == blockGroupStatus.UnconnectedBlockCount
            && blockGroupStatus.FallingBlockCount == 0)
        {
            uiManager.GameOver();
        }
    }
}
