using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class GameDirector : MonoBehaviour
{
    private GameObject gameManager;
    private GameObject cubeTemp;

    public CameraPos cameraPos;
    public GameObject[] cubePrefab;

    public void CreateCubeAndPattern(int modelIdx, int stageNum)
    {
        gameManager = Instantiate(Resources.Load<GameObject>("Prefabs/GameManager"), transform.parent);
        gameManager.GetComponent<GameManager>().blockType = stageNum == 0 ? IBlockType.BLOCK_TYPE.RANDOMIZED : IBlockType.BLOCK_TYPE.PATTERNED;
        cubeTemp = Instantiate(cubePrefab[modelIdx], transform.position, Quaternion.identity, transform.parent);
        cubeTemp.GetComponent<CubePattern>().SetPattern(stageNum);
        cameraPos.SetCameraDistance(modelIdx);
    }

    public void DestroyBlocks()
    {
        Destroy(cubeTemp);
        Destroy(gameManager);
    }
}
