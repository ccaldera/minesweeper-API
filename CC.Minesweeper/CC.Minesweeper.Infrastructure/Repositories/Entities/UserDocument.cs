namespace CC.Minesweeper.Infrastructure.Repositories.Entities
{
    /// <summary>
    /// The class representing a user in mongo.
    /// </summary>
    public class UserDocument : Document
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
