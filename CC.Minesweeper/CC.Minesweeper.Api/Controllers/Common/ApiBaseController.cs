using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Minesweeper.Api.Controllers.Common
{
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected string GetCurrentUserEmail()
        {
            if (User?.Identity?.IsAuthenticated != true)
                throw new UnauthorizedAccessException("User not authenticated.");

            var email = User?.FindFirst(ServiceConstants.EmailClaim)?.Value;

            return email;
        }

        protected string GetCurrentUserId()
        {
            if (User?.Identity?.IsAuthenticated != true)
                throw new UnauthorizedAccessException("User not authenticated.");

            var id = User?.FindFirst(ServiceConstants.IdClaim)?.Value;

            return id;
        }
    }
}
