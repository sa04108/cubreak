using UnityEngine;

namespace Cubreak
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform nextCamera;
        [SerializeField] private Transform cubeParent;

        float slerpVal;
        bool isUp;

        // Start is called before the first frame update
        void Start()
        {
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
                slerpVal += Time.deltaTime;

            transform.position = SlerpAround(transform.position, nextCamera.position, Vector3.up * nextCamera.position.y, slerpVal);
            transform.LookAt(cubeParent);
        }

        private Vector3 SlerpAround(Vector3 a, Vector3 b, Vector3 pivot, float t)
        {
            // A, B, P는 Vector3 타입 (월드 좌표계)
            Vector3 vA = a - pivot;                             // 피벗 기준 A 벡터
            Vector3 vB = b - pivot;                             // 피벗 기준 B 벡터

            // 1) 방향만 Slerp (R=일정한 반지름)
            Vector3 dirA = vA.normalized;
            Vector3 dirB = vB.normalized;
            Vector3 slerpedDir = Vector3.Slerp(dirA, dirB, t);   // t는 0~1

            // 2) 반지름(거리)도 선형 보간 (이유: 위 아래로 카메라 이동 시 반지름이 바뀐다.)
            float rA = vA.magnitude;
            float rB = vB.magnitude;
            float lerpedRadius = Mathf.Lerp(rA, rB, t);

            // 결과 위치는 피벗에 Slerp(보간)된 벡터를 더하면 된다.
            Vector3 result = pivot + slerpedDir * lerpedRadius;
            return result;
        }

        public void RotateLeft()
        {
            slerpVal = 0.0f;
            nextCamera.RotateAround(cubeParent.position, Vector3.up, 90f);
        }

        public void RotateRight()
        {
            slerpVal = 0.0f;
            nextCamera.RotateAround(cubeParent.position, Vector3.up, -90f);
        }

        public void RotateUp()
        {
            if (!isUp)
            {
                slerpVal = 0.0f;
                nextCamera.Translate(Vector3.one * 3f);
                isUp = true;
            }
        }

        public void RotateDown()
        {
            if (isUp)
            {
                slerpVal = 0.0f;
                nextCamera.Translate(Vector3.one * -3f);
                isUp = false;
            }
        }

        /// <summary>
        /// 대상 게임오브젝트들이 위치한 곳으로 카메라를 측면으로 회전시킵니다.
        /// </summary>
        public void RotateSideTowardObjects(GameObject[] objs)
        {
            Vector3 avgPos = Vector3.zero;
            foreach (GameObject obj in objs)
            {
                avgPos += obj.transform.position;
            }
            avgPos /= objs.Length;

            float minDist = float.MaxValue;
            int minIndex = 0;
            for (int i = 0; i < 4; i++)
            {
                RotateRight();

                float dist = (avgPos - nextCamera.position).magnitude;
                if (minDist > dist)
                {
                    minDist = dist;
                    minIndex = i;
                }
            }

            for (int i = 0; i < minIndex + 1; i++)
                RotateRight();
        }

        public void ResetPositionImmediately()
        {
            RotateDown();
            nextCamera.localPosition = Vector3.zero;
            nextCamera.localRotation = Quaternion.identity;
            transform.position = nextCamera.position;
            transform.rotation = nextCamera.rotation;
        }

        public void SetCameraDistance(int dimension)
        {
            dimension += 4;
            transform.parent.position = new Vector3(-dimension, 0.0f, -dimension);
        }
    } 
}
