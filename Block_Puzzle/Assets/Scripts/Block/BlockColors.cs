using UnityEngine;

namespace Cublocks
{
    public enum ENUM_COLOR
    {
        RED,
        GREEN,
        BLUE,
        YELLOW,
        MAGENTA,
        CYAN,
        PURPLE,
        WHITE
    }

    public class BlockColors
    {
        public static Color[] colors =
        {
            Color.red,
            Color.green,
            Color.blue,
            Color.yellow,
            Color.magenta,
            Color.cyan,
            Color.purple,
            Color.white // default color
        };
    }
}
