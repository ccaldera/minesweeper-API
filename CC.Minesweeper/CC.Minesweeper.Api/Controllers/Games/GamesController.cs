using AutoMapper;
using CC.Minesweeper.Api.Controllers.Common;
using CC.Minesweeper.Api.Controllers.Games.Models;
using CC.Minesweeper.Core.Domain.Entities;
using CC.Minesweeper.Core.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CC.Minesweeper.Api.Controllers.Games
{
    [Route("api/[controller]")]
    public class GamesController : ApiBaseController
    {
        private readonly GameService gameService;
        private readonly IMapper mapper;

        public GamesController(
            GameService gameService,
            IMapper mapper)
        {
            this.gameService = gameService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Creates a new game for the user.
        /// </summary>
        /// <param name="request">The game settings.</param>
        /// <returns>The new game.</returns>
        [Authorize]
        [HttpPost("new")]
        public async Task<IActionResult> Post(NewGameRequest request)
        {
            var userId = GetCurrentUserId();

            var result = await gameService.NewGame(
                userId, 
                request.Rows, 
                request.Columns, 
                request.Mines);

            var response = mapper.Map<Game, GameResponse>(result);

            return Ok(response);
        }

        /// <summary>
        /// Gets all the games register to the current user.
        /// </summary>
        /// <returns>The list of games.</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = GetCurrentUserId();

            var results = await gameService.GetGames(userId);

            var response = mapper.Map<IEnumerable<Game>, IEnumerable<GameResponse>>(results);

            return Ok(response);
        }

        /// <summary>
        /// Deletes a game.
        /// </summary>
        /// <param name="id">The game id to delete.</param>
        /// <returns>no content.</returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var userId = GetCurrentUserId();

            await gameService.DeleteGame(userId, id);

            return NoContent();
        }
    }
}
