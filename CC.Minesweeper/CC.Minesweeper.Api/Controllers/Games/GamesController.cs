using AutoMapper;
using CC.Minesweeper.Api.Controllers.Common;
using CC.Minesweeper.Api.Controllers.Games.Models;
using CC.Minesweeper.Core.Domain.Entities;
using CC.Minesweeper.Core.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CC.Minesweeper.Api.Controllers.Games
{
    /// <summary>
    /// The game endpoint, responsible for game related operations.
    /// </summary>
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
        /// <response code="200">Returns the game information.</response>
        [Authorize]
        [HttpPost("new")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GameResponse>> Post(NewGameRequest request)
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
        /// Reveals a given call for a game in progress.
        /// </summary>
        /// <param name="id">The game id.</param>
        /// <param name="request">The request containing the x and y references.</param>
        /// <returns>The updated game status.</returns>
        /// <response code="200">Returns the updated game information.</response>
        [Authorize]
        [HttpPatch("{id}/reveal")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GameResponse>> Patch(string id, RevealRequest request)
        {
            var userId = GetCurrentUserId();

            var result = await gameService.Reveal(
                userId,
                id,
                request.Row,
                request.Column);

            var response = mapper.Map<Game, GameResponse>(result);

            return Ok(response);
        }

        /// <summary>
        /// Marks a cell as flagged for a game in progress.
        /// </summary>
        /// <param name="id">The game id.</param>
        /// <param name="request">The request containing the x and y references.</param>
        /// <returns>The updated game status.</returns>
        /// <response code="200">Returns the updated game information.</response>
        [Authorize]
        [HttpPatch("{id}/switch-flag")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GameResponse>> PatchFlag(string id, RevealRequest request)
        {
            var userId = GetCurrentUserId();

            var result = await gameService.SwitchFlag(
                userId,
                id,
                request.Row,
                request.Column);

            var response = mapper.Map<Game, GameResponse>(result);

            return Ok(response);
        }

        /// <summary>
        /// Gets all the games register to the current user.
        /// </summary>
        /// <returns>The list of games.</returns>
        /// <response code="200">Returns the list of games associated to a user.</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GameResponse>>> Get()
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
        /// <response code="204">Returns no content for deleted games.</response>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(string id)
        {
            var userId = GetCurrentUserId();

            await gameService.DeleteGame(userId, id);

            return NoContent();
        }
    }
}
