using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class CameraRotation : MonoBehaviour
{
    public Transform nextCamera;
    public Button leftButton;
    public Button rightButton;

    Vector3 cubePos;
    float slerpVal;

    // Start is called before the first frame update
    void Start()
    {
        leftButton.onClick.AddListener(OnLeftClick);
        rightButton.onClick.AddListener(OnRightClick);
        cubePos = Vector3.down * 5;
        slerpVal = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (slerpVal < 1.0f)
            slerpVal += Time.deltaTime / 3.0f;

        transform.position = Vector3.Slerp(transform.position, nextCamera.position, slerpVal);
        transform.LookAt(cubePos);
    }
    
    void OnLeftClick()
    {
        slerpVal = 0.0f;
        nextCamera.RotateAround(cubePos, Vector3.up, 90f);
    }

    void OnRightClick()
    {
        slerpVal = 0.0f;
        nextCamera.RotateAround(cubePos, Vector3.up, -90f);
    }
}
