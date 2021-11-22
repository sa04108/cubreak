using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    public GameObject cubePrefab;

    private GameObject cubeTemp;

    private void OnEnable()
    {
        cubeTemp = Instantiate(cubePrefab, transform.position, Quaternion.identity, transform.parent);
    }

    private void OnDisable()
    {
        Destroy(cubeTemp);
    }
}
