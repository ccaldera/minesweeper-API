namespace CC.Minesweeper.Api.Controllers.Games.Models
{
    /// <summary>
    /// The reveal request.
    /// </summary>
    public class RevealRequest
    {
        /// <summary>
        /// Gets or set sthe row number.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Gets or sets the column number.
        /// </summary>
        public int Column { get; set; }
    }
}
