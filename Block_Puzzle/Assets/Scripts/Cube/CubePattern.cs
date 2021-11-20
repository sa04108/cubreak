using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePattern : MonoBehaviour
{
    public GameObject[] firstFloor;
    public GameObject[] secondFloor;
    public GameObject[] thirdFloor;

    private void Start()
    {
        SetFloorColor(firstFloor, new int[] { 1, 3, 4, 7, 9}, Color.red);
        SetFloorColor(firstFloor, new int[] { 2, 5, 8}, Color.yellow);
        SetFloorColor(firstFloor, new int[] { 6}, Color.blue);
        SetFloorColor(secondFloor, new int[] { 4}, Color.red);
        SetFloorColor(secondFloor, new int[] { 1, 5, 7}, Color.yellow);
        SetFloorColor(secondFloor, new int[] { 2, 3, 8, 9}, Color.blue);
        SetFloorColor(thirdFloor, new int[] { 2, 8}, Color.red);
        SetFloorColor(thirdFloor, new int[] { 4}, Color.yellow);
        SetFloorColor(thirdFloor, new int[] { 1, 3, 7, 9}, Color.blue);
    }

    /// <summary>
    /// list 에 들어갈 인덱스 번호는 1~9 입니다.
    /// </summary>
    /// <param name="floor"></param>
    /// <param name="list"></param>
    /// <param name="color"></param>
    private void SetFloorColor(GameObject[] floor, int[] list, Color color)
    {
        foreach (var idx in list)
        {
            floor[idx - 1].GetComponent<Renderer>().material.color = color;
        }
    }
}
