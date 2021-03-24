namespace CC.Minesweeper.Infrastructure.Repositories.Entities
{
    public class UserDocument : Document
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
