using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Floor
{
    public GameObject[] floor;
}

public class CubeBlocks : MonoBehaviour
{
    public List<Floor> floors = new List<Floor>();
}
