using Microsoft.AspNetCore.Mvc;
using System;

namespace CC.Minesweeper.Api.Controllers.Common
{
    /// <summary>
    /// The base controller.
    /// </summary>
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        /// <summary>
        /// Gets the current user email.
        /// </summary>
        /// <returns>the user email.</returns>
        protected string GetCurrentUserEmail()
        {
            if (User?.Identity?.IsAuthenticated != true)
                throw new UnauthorizedAccessException("User not authenticated.");

            var email = User?.FindFirst(ServiceConstants.EmailClaim)?.Value;

            return email;
        }

        /// <summary>
        /// Gets the user id.
        /// </summary>
        /// <returns>the user id.</returns>
        protected string GetCurrentUserId()
        {
            if (User?.Identity?.IsAuthenticated != true)
                throw new UnauthorizedAccessException("User not authenticated.");

            var id = User?.FindFirst(ServiceConstants.IdClaim)?.Value;

            return id;
        }
    }
}
