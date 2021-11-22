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
                SetFloorColor(firstFloor, new int[] { 1, 3 }, Color.red);
                SetFloorColor(firstFloor, new int[] { 2, 4 }, Color.blue);
                SetFloorColor(secondFloor, new int[] { 1, 2 }, Color.green);
                break;
            case 2:
                SetFloorColor(firstFloor, new int[] { 1, 3 }, Color.red);
                SetFloorColor(firstFloor, new int[] { 2 }, Color.green);
                SetFloorColor(firstFloor, new int[] { 4 }, Color.blue);
                SetFloorColor(secondFloor, new int[] { 1 }, Color.green);
                SetFloorColor(secondFloor, new int[] { 3 }, Color.blue);
                break;
            case 3:
                SetFloorColor(firstFloor, new int[] { 1, 3, 4, 7, 9 }, Color.red);
                SetFloorColor(firstFloor, new int[] { 2, 5, 8 }, Color.yellow);
                SetFloorColor(firstFloor, new int[] { 6 }, Color.blue);
                SetFloorColor(secondFloor, new int[] { 4 }, Color.red);
                SetFloorColor(secondFloor, new int[] { 1, 5, 7 }, Color.yellow);
                SetFloorColor(secondFloor, new int[] { 2, 3, 8, 9 }, Color.blue);
                SetFloorColor(thirdFloor, new int[] { 2, 8 }, Color.red);
                SetFloorColor(thirdFloor, new int[] { 4 }, Color.yellow);
                SetFloorColor(thirdFloor, new int[] { 1, 3, 7, 9 }, Color.blue);
                break;
            case 4:
                SetFloorColor(firstFloor, new int[] { 1, 5, 9 }, Color.red);
                SetFloorColor(firstFloor, new int[] { 4, 8 }, Color.green);
                SetFloorColor(firstFloor, new int[] { 2, 6 }, Color.blue);
                SetFloorColor(firstFloor, new int[] { 3 }, Color.white);
                SetFloorColor(secondFloor, new int[] { 2, 4, 6, 8 }, Color.red);
                SetFloorColor(secondFloor, new int[] { 1, 3, 5, 9 }, Color.white);
                SetFloorColor(thirdFloor, new int[] { 7 }, Color.green);
                SetFloorColor(thirdFloor, new int[] { 1, 2, 3 }, Color.blue);
                SetFloorColor(thirdFloor, new int[] { 4, 8 }, Color.white);
                break;
            default:
                break;
        }
    }
}
