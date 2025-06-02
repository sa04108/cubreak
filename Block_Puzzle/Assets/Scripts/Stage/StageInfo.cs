using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Cubreak
{
    // 전체 스테이지 정보
    public class CubeStage
    {
        // 1부터 순차적으로 부여되는 ID
        [JsonProperty("id")]
        public int Id { get; set; }

        // N×N×N 에서의 N
        [JsonProperty("dimension")]
        public int Dimension { get; set; }

        // 스테이지 난이도 (Easy, Medium, Hard 등)
        [JsonProperty("difficulty")]
        public string Difficulty { get; set; } = string.Empty;

        // 각 층별 색상 배치 목록
        [JsonProperty("layers")]
        public List<CubeLayer> Layers { get; set; } = new List<CubeLayer>();

        /// <summary>
        /// JSON 문자열을 Stage 객체로 역직렬화
        /// </summary>
        public static CubeStage[] FromJson(string json) =>
            JsonConvert.DeserializeObject<CubeStage[]>(json);

        /// <summary>
        /// Stage 객체를 JSON 문자열로 직렬화
        /// </summary>
        public string ToJson() =>
            JsonConvert.SerializeObject(this, Formatting.Indented);
    }

    // 한 개 층(layer) 정보
    public class CubeLayer
    {
        // 0부터 시작하는 층 인덱스 (floors[0] → 1층)
        [JsonProperty("index")]
        public int Index { get; set; }

        // 이 층에서 색상별로 조각을 모아둔 배치 리스트
        [JsonProperty("arrangements")]
        public List<Arrangement> Arrangements { get; set; } = new List<Arrangement>();
    }

    // 한 색상에 속한 조각 위치 정보
    public class Arrangement
    {
        // ENUM_COLOR 값 (Json 직렬화 시 문자열로 표현)
        [JsonProperty("color")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ENUM_COLOR Color { get; set; }

        // 이 색을 가진 조각들의 1-based 위치 인덱스 배열
        [JsonProperty("positions")]
        public int[] Positions { get; set; } = Array.Empty<int>();
    } 
}
