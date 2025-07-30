using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BirthdayTracker.Controllers
{
    [ApiController]
    [Route("api/")]
    public class BaseController : ControllerBase
    {
        protected Guid? UserId =>
            Guid.TryParse(User.FindFirstValue("userId"), out var userId)
            ? userId
            : null;
    }
}