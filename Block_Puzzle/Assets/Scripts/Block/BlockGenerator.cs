using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    private GameManager gameManager;
    private BlockGroupStatus blockGroupStatus;

    public GameObject blockFloor;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        blockGroupStatus = FindObjectOfType<BlockGroupStatus>();
        StartCoroutine(GenerateBlocks());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator GenerateBlocks()
    {
        while (true)
        {
            if (blockGroupStatus.FallingBlockCount == 0)
            {
                int passCount = 0;

                for (int i = 0; i < 3; i++)
                {
                    if (Physics.Raycast(new Vector3(-2.0f, 1.0f, 1.0f - i), Vector3.right, 4.0f))
                        break;
                    else
                        passCount++;
                }

                if (passCount >= 3)
                    Instantiate(blockFloor, transform.position, Quaternion.identity, transform.parent);
                else
                    gameManager.GameOverCheck();
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
