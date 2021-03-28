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

        public DateTime EndDate { get; set; }
    }
}
