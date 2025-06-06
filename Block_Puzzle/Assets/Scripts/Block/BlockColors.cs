using System;
using System.Linq;
using UnityEngine;

namespace Cubreak
{
    public enum ENUM_COLOR
    {
        RED,
        GREEN,
        BLUE,
        YELLOW,
        MAGENTA,
        CYAN,
        BLACK,
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
            Color.black,
            Color.white // default color
        };

        /// <summary>
        /// 무작위로 n개의 색상 인덱스를 골라서 List<int> 형태로 반환합니다.
        /// </summary>
        /// <param name="n">가져올 색상 개수</param>
        public static int[] GetRandomColorIndices(int n)
        {
            int m = colors.Length;
            if (n < 0 || n > m)
                throw new ArgumentOutOfRangeException(nameof(n), "m은 0 이상 n 이하이어야 합니다.");

            // 1) 0부터 m-1까지의 인덱스를 담은 배열을 만들고
            int[] indices = new int[m];
            for (int i = 0; i < m; i++)
                indices[i] = i;

            // 2) 메서드 내부에서 Random 객체를 생성
            var rand = new System.Random();

            // 3) Fisher–Yates 방식으로 앞쪽 n개만 무작위로 섞기
            for (int i = 0; i < n; i++)
            {
                // i부터 m-1 사이의 랜덤 위치 하나를 선택
                int swapIdx = rand.Next(i, m);
                // indices[i]와 indices[swapIdx]를 교환
                int tmp = indices[i];
                indices[i] = indices[swapIdx];
                indices[swapIdx] = tmp;
            }

            // 4) 앞쪽 n개의 값을 반환
            return indices.Take(n).ToArray();
        }
    }
}
