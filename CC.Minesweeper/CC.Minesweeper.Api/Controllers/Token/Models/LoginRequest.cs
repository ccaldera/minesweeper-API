namespace CC.Minesweeper.Api.Controllers.Token.Models
{
    /// <summary>
    /// The login request class.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets ors ets the password.
        /// </summary>
        public string Password { get; set; }
    }
}
