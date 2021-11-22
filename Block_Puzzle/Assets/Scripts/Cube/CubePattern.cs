using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePattern : CubeMaterial
{
    protected override void Start()
    {
        base.Start();

        if (UIManager.Instance.OnPatternGame && cube333)
            SetPattern1();
    }

    private void SetPattern1()
    {
        SetFloorColor(firstFloor, new int[] { 1, 3, 4, 7, 9 }, Color.red);
        SetFloorColor(firstFloor, new int[] { 2, 5, 8 }, Color.yellow);
        SetFloorColor(firstFloor, new int[] { 6 }, Color.blue);
        SetFloorColor(secondFloor, new int[] { 4 }, Color.red);
        SetFloorColor(secondFloor, new int[] { 1, 5, 7 }, Color.yellow);
        SetFloorColor(secondFloor, new int[] { 2, 3, 8, 9 }, Color.blue);
        SetFloorColor(thirdFloor, new int[] { 2, 8 }, Color.red);
        SetFloorColor(thirdFloor, new int[] { 4 }, Color.yellow);
        SetFloorColor(thirdFloor, new int[] { 1, 3, 7, 9 }, Color.blue);
    }
}
