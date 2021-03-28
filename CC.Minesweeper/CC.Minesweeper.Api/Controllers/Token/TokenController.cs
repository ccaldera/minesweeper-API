using CC.Minesweeper.Api.Controllers.Token.Models;
using CC.Minesweeper.Core.Domain.Services;
using CC.Minesweeper.Infrastructure.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CC.Minesweeper.Api.Controllers.Token
{
    /// <summary>
    /// The token endpoint, responsible for token auth login.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly SecurityConfiguration securityConfiguration;
        private readonly UsersService usersService;

        public TokenController(
            SecurityConfiguration securityConfiguration,
            UsersService usersService)
        {
            this.securityConfiguration = securityConfiguration;
            this.usersService = usersService;
        }

        /// <summary>
        /// Creates a new access token for the requested user credentials.
        /// </summary>
        /// <param name="request">The request containing the user credentials.</param>
        /// <returns>The user credentials.</returns>
        /// <response code="200">Returns the auth token for the requested credentials.</response>
        /// <response code="401">Returns 401 for invalid credentials.</response>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginResponse>> Post(LoginRequest request)
        {
            var user = await usersService.LoginAsync(request.Username, request.Password);

            if (user != null)
            {
                var claims = new[]
                {
                    new Claim(ServiceConstants.IdClaim, user.Id),
                    new Claim(ServiceConstants.EmailClaim, user.Email)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityConfiguration.Secret));

                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddMonths(1), signingCredentials: signIn);

                var response = new LoginResponse
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
                };

                return Ok(response);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
