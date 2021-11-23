using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMaterial : MonoBehaviour
{
    bool isAlpha;
    float alphaVal;

    protected bool cube222;
    protected bool cube333;
    protected bool cube444;

    protected GameObject[] firstFloor;
    protected GameObject[] secondFloor;
    protected GameObject[] thirdFloor;
    protected GameObject[] fourthFloor;

    Button seeThroughButton;

    private void Awake()
    {
        isAlpha = false;
        alphaVal = 0.0f;

        InitFloors();

        seeThroughButton = GameObject.Find("See Through Button").GetComponent<Button>();
        if (cube333)
        {
            seeThroughButton.onClick.AddListener(Set333CubeAlpha);
        }
        else if (cube444)
        {
            seeThroughButton.onClick.AddListener(Set444CubeAlpha);
        }
    }

    private void InitFloors()
    {
        CubeBlocks cubeBlocks = GetComponent<CubeBlocks>();

        cube222 = cubeBlocks.floors.Count == 2;
        cube333 = cubeBlocks.floors.Count == 3;
        cube444 = cubeBlocks.floors.Count == 4;

        firstFloor = cubeBlocks.floors[0].floor;
        secondFloor = cubeBlocks.floors[1].floor;

        if (cube333)
            thirdFloor = cubeBlocks.floors[2].floor;
        if (cube444)
        {
            thirdFloor = cubeBlocks.floors[2].floor;
            fourthFloor = cubeBlocks.floors[3].floor;
        }
    }

    public void Set333CubeAlpha()
    {
        SetFloorAlpha(firstFloor, new int[] { 1, 2, 3, 4, 6, 7, 8, 9 }, isAlpha ? 1.0f : alphaVal);
        SetFloorAlpha(secondFloor, new int[] { 1, 2, 3, 4, 6, 7, 8, 9 }, isAlpha ? 1.0f : alphaVal);
        SetFloorAlpha(thirdFloor, new int[] { 1, 2, 3, 4, 6, 7, 8, 9 }, isAlpha ? 1.0f : alphaVal);

        isAlpha = !isAlpha;
    }

    public void Set444CubeAlpha()
    {
        SetFloorAlpha(firstFloor, new int[] { 1, 2, 3, 4, 5, 8, 9, 12, 13, 14, 15, 16 }, isAlpha ? 1.0f : alphaVal);
        SetFloorAlpha(secondFloor, new int[] { 1, 2, 3, 4, 5, 8, 9, 12, 13, 14, 15, 16 }, isAlpha ? 1.0f : alphaVal);
        SetFloorAlpha(thirdFloor, new int[] { 1, 2, 3, 4, 5, 8, 9, 12, 13, 14, 15, 16 }, isAlpha ? 1.0f : alphaVal);
        SetFloorAlpha(fourthFloor, new int[] { 1, 2, 3, 4, 5, 8, 9, 12, 13, 14, 15, 16 }, isAlpha ? 1.0f : alphaVal);

        isAlpha = !isAlpha;
    }

    /// <summary>
    /// list 에 들어갈 인덱스 번호는 1~9 입니다.
    /// </summary>
    /// <param name="floor"></param>
    /// <param name="list"></param>
    /// <param name="color"></param>
    public void SetFloorAlpha(GameObject[] floor, int[] list, float alpha)
    {
        foreach (var idx in list)
        {
            if (floor[idx - 1] != null)
            {
                Color color = floor[idx - 1].GetComponent<Renderer>().material.color;
                color.a = alpha;
                floor[idx - 1].GetComponent<Renderer>().material.color = color;
            }
        }
    }

    /// <summary>
    /// list 에 들어갈 인덱스 번호는 1~9 입니다.
    /// </summary>
    /// <param name="floor"></param>
    /// <param name="list"></param>
    /// <param name="color"></param>
    public void SetFloorColor(GameObject[] floor, int[] list, Color color)
    {
        foreach (var idx in list)
        {
            if (floor[idx - 1] != null)
                floor[idx - 1].GetComponent<Renderer>().material.color = color;
        }
    }
}
