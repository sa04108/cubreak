using UnityEngine;
using UnityEngine.UI;

public class CubeMaterial : MonoBehaviour
{
    bool isAlpha;
    float alphaVal;

    protected bool cube222;
    protected bool cube333;
    protected bool cube444;

    protected CubeBlocks cubeBlocks;
    Button seeThroughButton;

    private void Awake()
    {
        cubeBlocks = GetComponent<CubeBlocks>();
        isAlpha = false;
        alphaVal = 0.0f;

        SelectCube();

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

    private void SelectCube()
    {
        cube222 = cubeBlocks.floors.Count == 2;
        cube333 = cubeBlocks.floors.Count == 3;
        cube444 = cubeBlocks.floors.Count == 4;
    }

    public void Set333CubeAlpha()
    {
        SetFloorAlpha(cubeBlocks.floors[0].floor, new int[] { 1, 2, 3, 4, 6, 7, 8, 9 }, isAlpha ? 1.0f : alphaVal);
        SetFloorAlpha(cubeBlocks.floors[1].floor, new int[] { 1, 2, 3, 4, 6, 7, 8, 9 }, isAlpha ? 1.0f : alphaVal);
        SetFloorAlpha(cubeBlocks.floors[2].floor, new int[] { 1, 2, 3, 4, 6, 7, 8, 9 }, isAlpha ? 1.0f : alphaVal);

        isAlpha = !isAlpha;
    }

    public void Set444CubeAlpha()
    {
        SetFloorAlpha(cubeBlocks.floors[0].floor, new int[] { 1, 2, 3, 4, 5, 8, 9, 12, 13, 14, 15, 16 }, isAlpha ? 1.0f : alphaVal);
        SetFloorAlpha(cubeBlocks.floors[1].floor, new int[] { 1, 2, 3, 4, 5, 8, 9, 12, 13, 14, 15, 16 }, isAlpha ? 1.0f : alphaVal);
        SetFloorAlpha(cubeBlocks.floors[2].floor, new int[] { 1, 2, 3, 4, 5, 8, 9, 12, 13, 14, 15, 16 }, isAlpha ? 1.0f : alphaVal);
        SetFloorAlpha(cubeBlocks.floors[3].floor, new int[] { 1, 2, 3, 4, 5, 8, 9, 12, 13, 14, 15, 16 }, isAlpha ? 1.0f : alphaVal);

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
    public void SetFloorColor(GameObject[] floor, int[] list, ENUM_COLOR colorEnum)
    {
        foreach (var idx in list)
        {
            if (floor[idx - 1] != null) {
                floor[idx - 1].GetComponent<Renderer>().material.color = BlockColors.colors[(int)colorEnum];
            }
        }
    }
}
