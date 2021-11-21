using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePattern : MonoBehaviour
{
    CubeMaterial material;

    GameObject[] firstFloor;
    GameObject[] secondFloor;
    GameObject[] thirdFloor;

    private void Start()
    {
        material = GetComponent<CubeMaterial>();

        InitFloors();
        SetPattern1();
    }

    private void InitFloors()
    {
        CubeBlocks cubeBlocks = GetComponent<CubeBlocks>();
        firstFloor = cubeBlocks.firstFloor;
        secondFloor = cubeBlocks.secondFloor;
        thirdFloor = cubeBlocks.thirdFloor;
    }

    private void SetPattern1()
    {
        material.SetFloorColor(firstFloor, new int[] { 1, 3, 4, 7, 9 }, Color.red);
        material.SetFloorColor(firstFloor, new int[] { 2, 5, 8 }, Color.yellow);
        material.SetFloorColor(firstFloor, new int[] { 6 }, Color.blue);
        material.SetFloorColor(secondFloor, new int[] { 4 }, Color.red);
        material.SetFloorColor(secondFloor, new int[] { 1, 5, 7 }, Color.yellow);
        material.SetFloorColor(secondFloor, new int[] { 2, 3, 8, 9 }, Color.blue);
        material.SetFloorColor(thirdFloor, new int[] { 2, 8 }, Color.red);
        material.SetFloorColor(thirdFloor, new int[] { 4 }, Color.yellow);
        material.SetFloorColor(thirdFloor, new int[] { 1, 3, 7, 9 }, Color.blue);
    }
}
