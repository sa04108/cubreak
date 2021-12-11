using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Transform nextCamera;

    Vector3 cubePos;
    float slerpVal;
    bool isUp;

    // Start is called before the first frame update
    void Start()
    {
        cubePos = Vector3.down * 7;
        slerpVal = 0.0f;
        isUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            RotateLeft();
        else if (Input.GetKeyDown(KeyCode.D))
            RotateRight();
        else if (Input.GetKeyDown(KeyCode.W))
            RotateUp();
        else if (Input.GetKeyDown(KeyCode.S))
            RotateDown();

        if (slerpVal < 1.0f)
            slerpVal += Time.deltaTime / 3.0f;

        transform.position = Vector3.Slerp(transform.position, nextCamera.position, slerpVal);
        transform.LookAt(cubePos);
    }

    private void OnEnable()
    {
        ResetPosition();
    }

    public void RotateLeft()
    {
        slerpVal = 0.0f;
        nextCamera.RotateAround(cubePos, Vector3.up, 90f);
    }

    public void RotateRight()
    {
        slerpVal = 0.0f;
        nextCamera.RotateAround(cubePos, Vector3.up, -90f);
    }

    public void RotateUp()
    {
        if (!isUp)
        {
            slerpVal = 0.0f;
            nextCamera.Translate(new Vector3(1.0f, 1.0f, 1.0f) * 3.0f);
            isUp = true;
        }
    }

    public void RotateDown()
    {
        if (isUp)
        {
            slerpVal = 0.0f;
            nextCamera.Translate(new Vector3(1.0f, 1.0f, 1.0f) * -3.0f);
            isUp = false;
        }
    }

    public void ResetPosition()
    {
        RotateDown();
        nextCamera.localPosition = Vector3.zero;
        nextCamera.localRotation = Quaternion.identity;
    }
}
