using AutoMapper;
using CC.Minesweeper.Api.Controllers.Common;
using CC.Minesweeper.Api.Controllers.Users.Models;
using CC.Minesweeper.Core.Domain.Entities;
using CC.Minesweeper.Core.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CC.Minesweeper.Api.Controllers.Users
{
    /// <summary>
    /// The users endpoint, responsible for users operations.
    /// </summary>
    [Route("api/[controller]")]
    public class UsersController : ApiBaseController
    {
        private readonly UsersService usersService;
        private readonly IMapper mapper;

        public UsersController(
            UsersService usersService,
            IMapper mapper)
        {
            this.usersService = usersService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Register a new user in the system.
        /// </summary>
        /// <param name="request">Contains the new user information.</param>
        /// <returns>No content returned.</returns>
        /// <response code="204">Returns no content for created users.</response>
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Post(RegistrationRequest request)
        {
            var user = mapper.Map<RegistrationRequest, User>(request);

            await usersService.RegisterUserAsync(user);

            return NoContent();
        }

        /// <summary>
        /// Gets the information of the current user.
        /// </summary>
        /// <returns>The user information.</returns>
        /// <response code="200">Returns the users data.</response>
        [Authorize]
        [HttpGet("me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserProfileResponse>> Get()
        {
            var email = GetCurrentUserEmail();

            var user = await usersService.GetUserAsync(email);

            var profile = mapper.Map<User, UserProfileResponse>(user);

            return Ok(profile);
        }

        /// <summary>
        /// Deletes the current user.
        /// </summary>
        /// <returns>No content.</returns>
        /// <response code="204">Returns no content for deleted users.</response>
        [Authorize]
        [HttpDelete("me")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete()
        {
            var email = GetCurrentUserEmail();

            await usersService.DeleteUser(email);

            return NoContent();
        }
    }
}
