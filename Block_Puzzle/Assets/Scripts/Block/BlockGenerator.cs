using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    private GameObject cubeTemp;

    public void CreateCubeAndPattern(GameObject cubePrefab, int stageNum)
    {
        cubeTemp = Instantiate(cubePrefab, transform.position, Quaternion.identity, transform.parent);
        cubeTemp.GetComponent<CubePattern>().SetPattern(stageNum);
    }

    public void DestroyBlocks()
    {
        Destroy(cubeTemp);
    }
}
