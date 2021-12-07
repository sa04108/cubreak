using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePattern : CubeMaterial
{
    public void SetPattern(int stageNum)
    {
        BlockColors color = new BlockColors();

        switch (stageNum)
        {
            case 1:
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 1, 2, 3, 4 }, color.colors[0]);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 1, 2, 3, 4 }, color.colors[2]);
                break;
            case 2:
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 1, 3 }, color.colors[0]);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 2, 4 }, color.colors[2]);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 1, 2 }, color.colors[1]);
                break;
            case 3:
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 1, 3, 4, 7, 9 }, color.colors[0]);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 2, 5, 8 }, color.colors[2]);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 6 }, color.colors[3]);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 4 }, color.colors[0]);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 1, 5, 7 }, color.colors[2]);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 2, 3, 8, 9 }, color.colors[3]);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 2, 8 }, color.colors[0]);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 4 }, color.colors[2]);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 1, 3, 7, 9 }, color.colors[3]);
                break;
            case 4:
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 1, 5, 9 }, color.colors[0]);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 4, 8 }, color.colors[1]);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 2, 6 }, color.colors[2]);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 3 }, color.colors[4]);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 2, 4, 6, 8 }, color.colors[0]);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 1, 3, 5, 9 }, color.colors[4]);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 7 }, color.colors[1]);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 1, 2, 3 }, color.colors[2]);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 4, 8 }, color.colors[4]);
                break;
            case 5:
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 3, 7 }, color.colors[0]);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 1, 2 }, color.colors[1]);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 4, 5, 6 }, color.colors[2]);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 8, 9 }, color.colors[3]);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 5, 6, 8 }, color.colors[0]);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 2, 4, 7 }, color.colors[1]);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 9 }, color.colors[2]);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 1 }, color.colors[3]);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 8, 9 }, color.colors[0]);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 1, 5 }, color.colors[1]);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 2, 3, 4, 7 }, color.colors[3]);
                break;
            case 6:
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 4, 7 }, color.colors[0]);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 5, 6 }, color.colors[1]);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 3, 8, 9 }, color.colors[2]);
                SetFloorColor(cubeBlocks.floors[0].floor, new int[] { 1, 2 }, color.colors[4]);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 5 }, color.colors[0]);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 6, 9 }, color.colors[3]);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 4 }, color.colors[4]);
                SetFloorColor(cubeBlocks.floors[1].floor, new int[] { 1, 2 }, color.colors[5]);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 8, 9 }, color.colors[0]);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 5 }, color.colors[1]);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 3 }, color.colors[2]);
                SetFloorColor(cubeBlocks.floors[2].floor, new int[] { 1 }, color.colors[4]);
                break;
            default:
                break;
        }
    }
}
