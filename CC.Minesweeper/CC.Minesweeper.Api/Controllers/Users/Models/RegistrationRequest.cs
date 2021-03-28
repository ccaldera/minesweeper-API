namespace CC.Minesweeper.Api.Controllers.Users.Models
{
    /// <summary>
    /// The registration request class.
    /// </summary>
    public class RegistrationRequest
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// gets ors ets the password.
        /// </summary>
        public string Password { get; set; }
    }
}
