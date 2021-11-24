using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePattern : CubeMaterial
{
    public void SetPattern(int stageNum)
    {
        switch (stageNum)
        {
            case 1:
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 1, 2, 3, 4 }, Color.red);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 1, 2, 3, 4 }, Color.blue);
                break;
            case 2:
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 1, 3 }, Color.red);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 2, 4 }, Color.blue);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 1, 2 }, Color.green);
                break;
            case 3:
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 1, 3, 4, 7, 9 }, Color.red);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 2, 5, 8 }, Color.yellow);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 6 }, Color.blue);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 4 }, Color.red);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 1, 5, 7 }, Color.yellow);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 2, 3, 8, 9 }, Color.blue);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 2, 8 }, Color.red);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 4 }, Color.yellow);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 1, 3, 7, 9 }, Color.blue);
                break;
            case 4:
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 1, 5, 9 }, Color.red);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 4, 8 }, Color.green);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 2, 6 }, Color.blue);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 3 }, Color.white);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 2, 4, 6, 8 }, Color.red);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 1, 3, 5, 9 }, Color.white);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 7 }, Color.green);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 1, 2, 3 }, Color.blue);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 4, 8 }, Color.white);
                break;
            case 5:
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 3, 7 }, Color.red);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 1, 2 }, Color.green);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 4, 5, 6 }, Color.blue);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 8, 9 }, Color.yellow);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 5, 6, 8 }, Color.red);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 2, 4, 7 }, Color.green);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 9 }, Color.blue);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 1 }, Color.yellow);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 8, 9 }, Color.red);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 1, 5 }, Color.green);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 2, 3, 4, 7 }, Color.yellow);
                break;
            default:
                break;
        }
    }
}
