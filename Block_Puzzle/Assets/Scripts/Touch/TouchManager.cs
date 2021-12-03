using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    bool checkReady;

    public CameraRotation mainCamera;

    private void Start()
    {
        checkReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        SlideCheck();
    }

    void SlideCheck()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    break;
                case TouchPhase.Moved:
                    float speedX = touch.deltaPosition.x / touch.deltaTime;
                    float speedY = touch.deltaPosition.y / touch.deltaTime;
                    if (checkReady)
                    {
                        if (Mathf.Abs(speedX) > 2000f && Mathf.Abs(speedY) < 2000f)
                        {
                            if (speedX < 0)
                                mainCamera.RotateRight();
                            else
                                mainCamera.RotateLeft();

                            checkReady = false;
                        }
                        else if (Mathf.Abs(speedX) < 2000f && Mathf.Abs(speedY) > 2000f)
                        {
                            if (speedY < 0)
                                mainCamera.RotateUp();
                            else
                                mainCamera.RotateDown();

                            checkReady = false;
                        }
                    }
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    Invoke("SetCheckReadyTrue", 0.2f);
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    break;
            }
        }
    }

    void SetCheckReadyTrue()
    {
        checkReady = true;
    }
}
