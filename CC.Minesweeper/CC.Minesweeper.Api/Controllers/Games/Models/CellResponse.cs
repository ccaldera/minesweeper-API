using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CC.Minesweeper.Api.Controllers.Games.Models
{
    /// <summary>
    /// The cell response.
    /// </summary>
    public class CellResponse
    {
        /// <summary>
        /// Gets or sets the cell status, the expected values are Hidden | Visible | Flagged.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public CellStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the value if it was already revealed, -1 for mine, 1-8 for a mine neighbour and null for hidden cells.
        /// </summary>
        public int? Value { get; set; }
    }
}
