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
        /// Gets or sets the cell status.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public CellStatus Status { get; set; }
        
        /// <summary>
        /// Gets or sets the value if it was already revealed.
        /// </summary>
        public int? Value { get; set; }
    }
}
