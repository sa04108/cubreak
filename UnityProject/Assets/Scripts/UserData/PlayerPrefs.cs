using UnityEngine;

namespace Cubreak
{
    public enum ENUM_PLAYERPREFS
    {
        FrameRate,
        NumOfBlockColor,
        ExerciseDimension,
        ClearedStage
    }

    public class CustomPlayerPrefs
    {
        public const int DefaultFrameRate = 30;
        public const int DefaultNumOfBlockColor = 4;
        public const int DefaultExerciseDimension = 3;
        public const int DefaultClearedStage = 0;

        public static int GetInt(ENUM_PLAYERPREFS key, int? defaultValue = null)
        {
            if (defaultValue.HasValue)
            {
                return PlayerPrefs.GetInt(key.ToString(), defaultValue.Value);
            }
            else
            {
                return PlayerPrefs.GetInt(key.ToString());
            }
        }

        public static float GetFloat(ENUM_PLAYERPREFS key, float? defaultValue = null)
        {
            if (defaultValue.HasValue)
            {
                return PlayerPrefs.GetFloat(key.ToString(), defaultValue.Value);
            }
            else
            {
                return PlayerPrefs.GetFloat(key.ToString());
            }
        }

        public static void SetInt(ENUM_PLAYERPREFS key, int value)
        {
            PlayerPrefs.SetInt(key.ToString(), value);
        }

        public static void SetFloat(ENUM_PLAYERPREFS key, float value)
        {
            PlayerPrefs.SetFloat(key.ToString(), value);
        }

        public static void SetDefaultValues()
        {
            SetInt(ENUM_PLAYERPREFS.FrameRate, DefaultFrameRate);
            SetInt(ENUM_PLAYERPREFS.NumOfBlockColor, DefaultNumOfBlockColor);
            SetInt(ENUM_PLAYERPREFS.ExerciseDimension, DefaultExerciseDimension);
        }
    }
}
