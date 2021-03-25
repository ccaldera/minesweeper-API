namespace CC.Minesweeper.Api.Controllers.Games.Models
{
    public class NewGameRequest
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int Mines { get; set; }
    }
}
