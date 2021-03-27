using CC.Minesweeper.Core.Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace CC.Minesweeper.Api.Controllers.Games.Models
{
    public class GameResponse
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }

        public CellResponse[,] Board { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public GameStatusResponse Status { get; set; }

        public DateTime CreationDate { get; set; }

        public GameResponse(Game game)
        {
            Id = game.Id;
            UserId = game.Id;
            CreationDate = game.CreationDate;
            Rows = game.Rows;
            Columns = game.Columns;

            Board = new CellResponse[game.Rows, game.Columns];

            Status = (GameStatusResponse)Enum.Parse(typeof(GameStatusResponse), game.Status.ToString());

            for (int x = 0; x < game.Rows; x++)
            {
                for (int y = 0; y < game.Columns; y++)
                {
                    Board[x, y] = new CellResponse();

                    if (game.Board[x, y].IsRevealed)
                    {
                        Board[x, y].Status = CellStatus.Visible;
                        Board[x, y].Value = game.Board[x, y].Value;
                    }
                    else if(game.Board[x, y].IsFlagged)
                    {
                        Board[x, y].Status = CellStatus.Flagged;
                    }
                    else
                    {
                        Board[x, y].Status = CellStatus.Hidden;
                    }
                }
            }
        }
    }
}
