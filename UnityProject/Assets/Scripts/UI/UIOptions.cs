using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cubreak
{
    public class UIOptions : MonoBehaviour
    {
        [Header("Volume")]
        [SerializeField] private Slider volumeSlider;

        [Header("Frame Rate")]
        [SerializeField] private Button frameSwitch;
        [SerializeField] private Sprite[] frameSprites;
        [SerializeField] private TMP_Text[] frameTexts;

        [Header("Exercise Tab")]
        [SerializeField] private GameObject exerciseTab;
        [SerializeField] private TMP_Text cubeDimentionText;
        [SerializeField] private TMP_Text numOfBlockColorText;

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

        private void Start()
        {
            // NOTE
            // https://docs.unity3d.com/6000.1/Documentation/ScriptReference/Application-targetFrameRate.html
            // Desktop 플래폼에서는 vSync = 0일때에만 Target FrameRate가 작동함
            // Android, IOS는 vSync를 항상 무시함
            // Desktop과 Mobile에서의 동일한 애니메이션 속도를 제공하기 위해 동일한 프레임으로 고정하는 것을 권장
            QualitySettings.vSyncCount = 0;

            volumeSlider.onValueChanged.AddListener(SetVolume);
            volumeSlider.value = CustomPlayerPrefs.GetFloat(ENUM_PLAYERPREFS.Volume, CustomPlayerPrefs.DefaultVolume);

            int frameRate = CustomPlayerPrefs.GetInt(ENUM_PLAYERPREFS.FrameRate, CustomPlayerPrefs.DefaultFrameRate);
            SetFrameRate(frameRate);
            frameSwitch.onClick.AddListener(ToggleFrameRate);
#if DEBUG
            exerciseTab.SetActive(true);

            ExerciseDimension = CustomPlayerPrefs.GetInt(ENUM_PLAYERPREFS.ExerciseDimension, CustomPlayerPrefs.DefaultExerciseDimension);
            NumOfBlockColor = CustomPlayerPrefs.GetInt(ENUM_PLAYERPREFS.NumOfBlockColor, CustomPlayerPrefs.DefaultNumOfBlockColor);

            SetExerciseDimension(ExerciseDimension);
            SetNumberOfBlockColor(NumOfBlockColor);
#endif
        }

        private void SetVolume(float volume)
        {
            AudioManager.Instance.SetVolume(volume);
            CustomPlayerPrefs.SetFloat(ENUM_PLAYERPREFS.Volume, volume);
        }

        private void ToggleFrameRate()
        {
            int frameRate = CustomPlayerPrefs.GetInt(ENUM_PLAYERPREFS.FrameRate, CustomPlayerPrefs.DefaultFrameRate);
            if (frameRate == 30)
            {
                SetFrameRate(60);
            }
            else if (frameRate == 60)
            {
                SetFrameRate(30);
            }
        }

        private void SetFrameRate(int frameRate)
        {
            if (frameRate == 30)
            {
                Application.targetFrameRate = 30;
                frameSwitch.image.sprite = frameSprites[0];
                frameTexts[0].gameObject.SetActive(true);
                frameTexts[1].gameObject.SetActive(false);
            }
            else if (frameRate == 60)
            {
                Application.targetFrameRate = 60;
                frameSwitch.image.sprite = frameSprites[1];
                frameTexts[0].gameObject.SetActive(false);
                frameTexts[1].gameObject.SetActive(true);
            }

            CustomPlayerPrefs.SetInt(ENUM_PLAYERPREFS.FrameRate, frameRate);
        }



        private void SetExerciseDimension(int dimension)
        {
            ExerciseDimension = dimension;
            cubeDimentionText.text = $"{ExerciseDimension}x{ExerciseDimension}x{ExerciseDimension}";
            CustomPlayerPrefs.SetInt(ENUM_PLAYERPREFS.ExerciseDimension, ExerciseDimension);
        }

        private void SetNumberOfBlockColor(int colors)
        {
            NumOfBlockColor = colors;
            numOfBlockColorText.text = $"{NumOfBlockColor}";
            CustomPlayerPrefs.SetInt(ENUM_PLAYERPREFS.NumOfBlockColor, NumOfBlockColor);
        }

        // Button Linked
        public void SetExerciseDimensionDelta(int delta)
        {
            SetExerciseDimension(ExerciseDimension + delta);
        }

        // Button Linked
        public void SetNumberOfBlockColorDelta(int delta)
        {
            SetNumberOfBlockColor(NumOfBlockColor + delta);
        }

        // Button Linked
        public void ResetOptions()
        {
            CustomPlayerPrefs.SetDefaultValues();

            volumeSlider.value = CustomPlayerPrefs.DefaultVolume;
            SetVolume(CustomPlayerPrefs.DefaultVolume);
            SetFrameRate(CustomPlayerPrefs.DefaultFrameRate);
            SetExerciseDimension(CustomPlayerPrefs.DefaultExerciseDimension);
            SetNumberOfBlockColor(CustomPlayerPrefs.DefaultNumOfBlockColor);
        }
    }
}