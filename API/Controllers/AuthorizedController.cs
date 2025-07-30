using BirthdayTracker.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    public abstract class AuthorizedController : BaseController
    {
        protected Guid AuthorizedUserId =>
            UserId ?? throw new UnauthorizedAccessException("User ID not found in token");
    }
}