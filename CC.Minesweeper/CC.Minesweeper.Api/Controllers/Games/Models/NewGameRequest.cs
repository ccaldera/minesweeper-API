namespace CC.Minesweeper.Api.Controllers.Games.Models
{
    /// <summary>
    /// The new game request.
    /// </summary>
    public class NewGameRequest
    {
        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        public int Columns { get; set; }

        /// <summary>
        /// Gets or sets the mines.
        /// </summary>
        public int Mines { get; set; }
    }
}
