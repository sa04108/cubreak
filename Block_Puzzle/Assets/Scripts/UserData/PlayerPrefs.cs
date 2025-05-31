using UnityEngine;

namespace Cublocks
{
    public enum ENUM_PLAYERPREFS
    {
        BlockFallingSpeed,
        NumOfBlockColor,
        ExerciseDimension,
        ClearedStage
    }

    public class CustomPlayerPrefs
    {
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
    }
}
