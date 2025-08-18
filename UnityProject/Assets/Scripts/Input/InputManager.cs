using UnityEngine;
using UnityEngine.Events;

namespace Cubreak
{
    public class InputManager : Singleton<InputManager>
    {
        [SerializeField] new CameraController camera;
        bool inputReady;
        Vector2 startPos;
        Vector2 currentPos;
        Vector2 endPos;

        public UnityEvent OnClick;
        public UnityEvent OnSlideLeft;
        public UnityEvent OnSlideRight;
        public UnityEvent OnSlideUp;
        public UnityEvent OnSlideDown;

        private void Start()
        {
            inputReady = true;
            startPos = Vector2.zero;
            currentPos = Vector2.zero;
            endPos = Vector2.zero;
        }

        // Update is called once per frame
        void Update()
        {
            Click();
            Slide();
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

        private void Click()
        {
            if (!inputReady)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                startPos = Input.mousePosition;
                currentPos = startPos;
            }

            if (Input.GetMouseButtonUp(0))
            {
                endPos = Input.mousePosition;
                if (Vector2.Distance(startPos, endPos) < Screen.width * 0.1f)
                {
                    OnClick?.Invoke();
                }
            }
        }

        private void Slide()
        {
            if (Input.touchSupported)
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

                            CheckSlide(speedX, speedY);
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
            }
            else
            {
                Vector2 prevPos = currentPos;

                if (Input.GetMouseButton(0))
                {
                    currentPos = Input.mousePosition;
                }

                float deltaTime = Time.deltaTime;
                float speedX = (currentPos.x - prevPos.x) / deltaTime;
                float speedY = (currentPos.y - prevPos.y) / deltaTime;

                if (CheckSlide(speedX, speedY))
                    Invoke("SetInputReadyTrue", 0.2f);
            }
        }

        private bool CheckSlide(float speedX, float speedY)
        {
            if (inputReady)
            {
                if (Mathf.Abs(speedX) > 1500f && Mathf.Abs(speedY) < 1500f)
                {
                    if (speedX < 0)
                        OnSlideRight?.Invoke();
                    else
                        OnSlideLeft?.Invoke();

                    inputReady = false;
                    return true;
                }
                else if (Mathf.Abs(speedX) < 1500f && Mathf.Abs(speedY) > 1500f)
                {
                    if (speedY < 0)
                        OnSlideUp?.Invoke();
                    else
                        OnSlideDown?.Invoke();

                    inputReady = false;
                    return true;
                }
            }

            return false;
        }

        private void SetInputReadyTrue()
        {
            inputReady = true;
        }
    } 
}
