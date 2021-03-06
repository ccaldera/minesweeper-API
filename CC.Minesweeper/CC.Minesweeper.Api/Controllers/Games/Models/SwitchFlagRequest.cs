namespace CC.Minesweeper.Api.Controllers.Games.Models
{
    /// <summary>
    /// The switch flag request.
    /// </summary>
    public class SwitchFlagRequest
    {
        /// <summary>
        /// Gets ot sets the row number to flag.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Gets or sets the colulmn number to flag.
        /// </summary>
        public int Column { get; set; }
    }
}
