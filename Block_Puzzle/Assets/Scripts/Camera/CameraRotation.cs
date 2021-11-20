using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class CameraRotation : MonoBehaviour
{
    public Transform nextCamera;
    public Button leftButton;
    public Button rightButton;

    Vector3 velocity;
    Vector3 cubePos;

    bool clockwise;
    bool turn;

    // Start is called before the first frame update
    void Start()
    {
        leftButton.onClick.AddListener(OnLeftClick);
        rightButton.onClick.AddListener(OnRightClick);
        velocity = Vector3.zero;
        cubePos = Vector3.zero;
        clockwise = true;
        turn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (turn)
            transform.RotateAround(cubePos, Vector3.up, clockwise ? Time.deltaTime * 300.0f : -Time.deltaTime * 300.0f);
        transform.LookAt(cubePos);
    }
    
    void OnLeftClick()
    {
        clockwise = true;
        nextCamera.RotateAround(cubePos, Vector3.up, 90f);
    }

    void OnRightClick()
    {
        clockwise = false;
        nextCamera.RotateAround(cubePos, Vector3.up, -90f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NextCamera"))
            turn = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NextCamera"))
            turn = true;
    }
}
