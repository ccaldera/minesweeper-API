namespace CC.Minesweeper.Core.Domain.Entities
{
    public class User : IEntity
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
