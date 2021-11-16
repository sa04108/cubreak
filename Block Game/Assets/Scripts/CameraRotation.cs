using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRotation : MonoBehaviour
{
    public Transform nextCamera;
    public Button leftButton;
    public Button rightButton;

    Vector3 velocity;
    Vector3 cubePos;

    // Start is called before the first frame update
    void Start()
    {
        leftButton.onClick.AddListener(OnLeftClick);
        rightButton.onClick.AddListener(OnRightClick);
        velocity = Vector3.zero;
        cubePos = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, nextCamera.position, ref velocity, 0.2f);
        transform.LookAt(cubePos);
    }
    
    void OnLeftClick()
    {
        nextCamera.RotateAround(cubePos, Vector3.up, 90f);
    }

    void OnRightClick()
    {
        nextCamera.RotateAround(cubePos, Vector3.up, -90f);
    }
}
