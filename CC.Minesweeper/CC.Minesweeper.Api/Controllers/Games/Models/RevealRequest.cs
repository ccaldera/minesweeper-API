namespace CC.Minesweeper.Api.Controllers.Games.Models
{
    /// <summary>
    /// The reveal request.
    /// </summary>
    public class RevealRequest
    {
        /// <summary>
        /// Gets or set the row number to reveal.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Gets or sets the column number to reveal.
        /// </summary>
        public int Column { get; set; }
    }
}
