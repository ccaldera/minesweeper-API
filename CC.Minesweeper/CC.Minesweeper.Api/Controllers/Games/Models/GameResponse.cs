using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace CC.Minesweeper.Api.Controllers.Games.Models
{
    /// <summary>
    /// The game response.
    /// </summary>
    public class GameResponse
    {
        /// <summary>
        /// Gets or sets the game id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the rows
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        public int Columns { get; set; }

        /// <summary>
        /// Gets or sets the board.
        /// </summary>
        public CellResponse[,] Board { get; set; }

        /// <summary>
        /// Gets or sets the game status.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public GameStatusResponse Status { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the game.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the game.
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
