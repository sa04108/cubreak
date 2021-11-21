using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMaterial : MonoBehaviour
{
    bool isAlpha;
    float alphaVal;

    GameObject[] firstFloor;
    GameObject[] secondFloor;
    GameObject[] thirdFloor;

    Button seeThroughButton;

    private void Start()
    {
        isAlpha = false;
        alphaVal = 0.0f;

        InitFloors();

        seeThroughButton = GameObject.Find("See Through Button").GetComponent<Button>();
        seeThroughButton.onClick.AddListener(SetCubeAlpha);

        if (UIManager.Instance.OnPatternGame)
            gameObject.AddComponent<CubePattern>();
    }

    private void InitFloors()
    {
        CubeBlocks cubeBlocks = GetComponent<CubeBlocks>();
        firstFloor = cubeBlocks.firstFloor;
        secondFloor = cubeBlocks.secondFloor;
        thirdFloor = cubeBlocks.thirdFloor;
    }

    public void SetCubeAlpha()
    {
        SetFloorAlpha(firstFloor, new int[] { 1, 2, 3, 4, 6, 7, 8, 9 }, isAlpha ? 1.0f : alphaVal);
        SetFloorAlpha(secondFloor, new int[] { 1, 2, 3, 4, 6, 7, 8, 9 }, isAlpha ? 1.0f : alphaVal);
        SetFloorAlpha(thirdFloor, new int[] { 1, 2, 3, 4, 6, 7, 8, 9 }, isAlpha ? 1.0f : alphaVal);

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
            Color color = floor[idx - 1].GetComponent<Renderer>().material.color;
            color.a = alpha;
            floor[idx - 1].GetComponent<Renderer>().material.color = color;
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
            floor[idx - 1].GetComponent<Renderer>().material.color = color;
        }
    }
}
