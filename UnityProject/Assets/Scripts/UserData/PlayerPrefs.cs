using UnityEngine;

namespace Cubreak
{
    public enum ENUM_PLAYERPREFS
    {
        Volume,
        FrameRate,
        NumOfBlockColor,
        ExerciseDimension,
        ClearedStage,
        JWT_Token
    }

    public class CustomPlayerPrefs
    {
        public const float DefaultVolume = 0.5f;
        public const int DefaultFrameRate = 30;
        public const int DefaultNumOfBlockColor = 4;
        public const int DefaultExerciseDimension = 3;
        public const int DefaultClearedStage = 0;
        public const string DefaultJWTToken = "";

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

        public static string GetString(ENUM_PLAYERPREFS key, string defaultValue = null)
        {
            if (!string.IsNullOrEmpty(defaultValue))
            {
                return PlayerPrefs.GetString(key.ToString(), defaultValue);
            }
            else
            {
                return PlayerPrefs.GetString(key.ToString());
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

        public static void SetString(ENUM_PLAYERPREFS key, string value)
        {
            PlayerPrefs.SetString(key.ToString(), value);
        }

        public static void SetDefaultValues()
        {
            SetFloat(ENUM_PLAYERPREFS.Volume, DefaultVolume);
            SetInt(ENUM_PLAYERPREFS.FrameRate, DefaultFrameRate);
            SetInt(ENUM_PLAYERPREFS.NumOfBlockColor, DefaultNumOfBlockColor);
            SetInt(ENUM_PLAYERPREFS.ExerciseDimension, DefaultExerciseDimension);
            SetInt(ENUM_PLAYERPREFS.ClearedStage, DefaultClearedStage);
        }
    }
}
