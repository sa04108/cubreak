using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class GameDirector : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private GameObject cubeTemp;

    public CameraPos cameraPos;
    public GameObject[] cubePrefab;

    public void CreateCubeAndPattern(int modelIdx, int stageNum)
    {
        gameManager.blockType = stageNum == 0 ? ENUM_BLOCK_TYPE.RANDOMIZED : ENUM_BLOCK_TYPE.PATTERNED;
        gameManager.gameObject.SetActive(true);
        cubeTemp = Instantiate(cubePrefab[modelIdx], transform.position, Quaternion.identity, transform.parent);
        cubeTemp.GetComponent<CubePattern>().SetPattern(stageNum);
        cameraPos.SetCameraDistance(modelIdx);
    }

    public void DestroyBlocks()
    {
        Destroy(cubeTemp);
        gameManager.gameObject.SetActive(false);
    }
}
