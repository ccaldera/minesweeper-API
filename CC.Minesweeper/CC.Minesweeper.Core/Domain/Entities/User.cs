namespace CC.Minesweeper.Core.Domain.Entities
{
    /// <summary>
    /// The user entity.
    /// </summary>
    public class User : IEntity
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }
    }
}
