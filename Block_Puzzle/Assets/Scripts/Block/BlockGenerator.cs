using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    private GameObject cubeTemp;

    public CameraPos cameraPos;
    public GameObject[] cubePrefab;

    public void CreateCubeAndPattern(int modelIdx, int stageNum)
    {
        cubeTemp = Instantiate(cubePrefab[modelIdx], transform.position, Quaternion.identity, transform.parent);
        cubeTemp.GetComponent<CubePattern>().SetPattern(stageNum);
        cameraPos.SetCameraDistance(modelIdx);
    }

    public void DestroyBlocks()
    {
        Destroy(cubeTemp);
    }
}
