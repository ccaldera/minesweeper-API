using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CC.Minesweeper.Api.Controllers.Games.Models
{
    public class CellResponse
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public CellStatus Status { get; set; }
        public int? Value { get; set; }
    }
}
