namespace CC.Minesweeper.Api.Controllers.Games.Models
{
    /// <summary>
    /// The new game request.
    /// </summary>
    public class NewGameRequest
    {
        /// <summary>
        /// Gets or sets the rows of the new game.
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// Gets or sets the columns of the new game.
        /// </summary>
        public int Columns { get; set; }

        /// <summary>
        /// Gets or sets the mines of the new game.
        /// </summary>
        public int Mines { get; set; }
    }
}
