namespace CC.Minesweeper.Core.Domain.Entities
{
    public class Game : IEntity
    {
        public string Id { get; set; }

        public string UserId { get; set; }
    }
}
