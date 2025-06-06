using TMPro;
using UnityEngine;

namespace Cubreak
{
    public class UIOptions : MonoBehaviour
    {
        [Header("Block Falling Speed Text")]
        [SerializeField] private TMP_Text blockFallingSpeedText;

        [Header("Exercise Tab")]
        [SerializeField] private GameObject exerciseTab;
        [SerializeField] private TMP_Text cubeDimentionText;
        [SerializeField] private TMP_Text numOfBlockColorText;

        private float blockFallingSpeed;
        public float BlockFallingSpeed
        {
            get => blockFallingSpeed;
            set
            {
                blockFallingSpeed = value;
                if (value < 1.0f) blockFallingSpeed = 1.0f;
                else if (value > 10.0f) blockFallingSpeed = 10.0f;
            }
        }

        private int exerciseDimension;
        public int ExerciseDimension
        {
            get => exerciseDimension;
            set
            {
                exerciseDimension = value;
                if (exerciseDimension < 2) exerciseDimension = 2;
                else if (exerciseDimension > 4) exerciseDimension = 4;
            }
        }

        private int numOfBlockColor;
        public int NumOfBlockColor
        {
            get => numOfBlockColor;
            set
            {
                numOfBlockColor = value;
                if (value < 1) numOfBlockColor = 1;
                else if (value > BlockColors.colors.Length) numOfBlockColor = BlockColors.colors.Length;
            }
        }

        private void Awake()
        {
            BlockFallingSpeed = CustomPlayerPrefs.GetFloat(ENUM_PLAYERPREFS.BlockFallingSpeed, 3f);
#if DEBUG
            ExerciseDimension = CustomPlayerPrefs.GetInt(ENUM_PLAYERPREFS.ExerciseDimension, 3);
            NumOfBlockColor = CustomPlayerPrefs.GetInt(ENUM_PLAYERPREFS.NumOfBlockColor, 4);
#endif
        }

        private void Start()
        {
            SetBlockFallingSpeed(0);
#if DEBUG
            exerciseTab.SetActive(true);
            SetExerciseDimension(0);
            SetNumberOfBlockColor(0);
#endif
        }

        public void SetBlockFallingSpeed(float delta)
        {
            BlockFallingSpeed += delta;
            blockFallingSpeedText.text = BlockFallingSpeed.ToString();
            CustomPlayerPrefs.SetFloat(ENUM_PLAYERPREFS.BlockFallingSpeed, BlockFallingSpeed);
        }

        public void SetExerciseDimension(int delta)
        {
            ExerciseDimension += delta;
            cubeDimentionText.text = ExerciseDimension + "x" + ExerciseDimension + "x" + ExerciseDimension;
            CustomPlayerPrefs.SetInt(ENUM_PLAYERPREFS.ExerciseDimension, ExerciseDimension);
        }

        public void SetNumberOfBlockColor(int delta)
        {
            NumOfBlockColor += delta;
            numOfBlockColorText.text = NumOfBlockColor.ToString();
            CustomPlayerPrefs.SetInt(ENUM_PLAYERPREFS.NumOfBlockColor, NumOfBlockColor);
        }
    }
}