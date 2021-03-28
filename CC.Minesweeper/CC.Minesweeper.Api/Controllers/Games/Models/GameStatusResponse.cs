namespace CC.Minesweeper.Api.Controllers.Games.Models
{
    /// <summary>
    /// The game status enum.
    /// </summary>
    public enum GameStatusResponse
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// InProgress
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// Complete
        /// </summary>
        Complete = 2,

        /// <summary>
        /// Failed
        /// </summary>
        Failed = 3
    }
}
