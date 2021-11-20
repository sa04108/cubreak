using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    public GameObject cubePrefab;

    private GameObject cubeTemp;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        cubeTemp = Instantiate(cubePrefab, transform.position, Quaternion.identity, transform.parent);
    }

    private void OnDisable()
    {
        Destroy(cubeTemp);
    }
}
