using AutoMapper;
using CC.Minesweeper.Api.Controllers.Common;
using CC.Minesweeper.Api.Controllers.Users.Models;
using CC.Minesweeper.Core.Domain.Entities;
using CC.Minesweeper.Core.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CC.Minesweeper.Api.Controllers.Users
{
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
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Post(RegistrationRequest request)
        {
            var user = mapper.Map<RegistrationRequest, User>(request);

            await usersService.RegisterUserAsync(user);

            return NoContent();
        }

        /// <summary>
        /// Gets the information of the current user.
        /// </summary>
        /// <returns>The user information.</returns>
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Get()
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
        [Authorize]
        [HttpDelete("me")]
        public async Task<IActionResult> Delete()
        {
            var email = GetCurrentUserEmail();

            await usersService.DeleteUser(email);

            return NoContent();
        }
    }
}
