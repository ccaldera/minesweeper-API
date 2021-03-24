using CC.Minesweeper.Api.Controllers.Token.Models;
using CC.Minesweeper.Core.Domain.Services;
using CC.Minesweeper.Infrastructure.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CC.Minesweeper.Api.Controllers.Token
{
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

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(LoginRequest request)
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

                var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

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
