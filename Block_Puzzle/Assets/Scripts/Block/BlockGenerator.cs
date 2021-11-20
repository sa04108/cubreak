using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    public GameObject blockFloor;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateBlocks()
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
            Instantiate(blockFloor, new Vector3(0.0f, 1.0f, 0.0f), Quaternion.identity, transform.parent);
    }
}
