using UnityEngine;

namespace Cublocks
{
    public class InputManager : Singleton<InputManager>
    {
        [SerializeField] new CameraController camera;
        bool inputReady;
        Vector2 startPos;
        Vector2 endPos;

        private void Start()
        {
            inputReady = true;
            startPos = Vector2.zero;
            endPos = Vector2.zero;
        }

        // Update is called once per frame
        void Update()
        {
            Escape();
        }

        private void Escape()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            }
        }

        public bool Click()
        {
            if (!inputReady)
                return false;

            if (Input.GetMouseButtonDown(0))
            {
                startPos = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                endPos = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (Vector2.Distance(startPos, endPos) < Screen.width * 0.1f)
                {
                    return true;
                }
            }

            return false;
        }

        public bool Slide()
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
                        if (inputReady)
                        {
                            if (Mathf.Abs(speedX) > 1500f && Mathf.Abs(speedY) < 1500f)
                            {
                                if (speedX < 0)
                                    camera.RotateRight();
                                else
                                    camera.RotateLeft();

                                inputReady = false;
                                return true;
                            }
                            else if (Mathf.Abs(speedX) < 1500f && Mathf.Abs(speedY) > 1500f)
                            {
                                if (speedY < 0)
                                    camera.RotateUp();
                                else
                                    camera.RotateDown();

                                inputReady = false;
                                return true;
                            }
                        }
                        break;
                    case TouchPhase.Stationary:
                        break;
                    case TouchPhase.Ended:
                        Invoke("SetInputReadyTrue", 0.2f);
                        break;
                    case TouchPhase.Canceled:
                        break;
                    default:
                        break;
                }
            }

            return false;
        }

        void SetInputReadyTrue()
        {
            inputReady = true;
        }
    } 
}
