using System.Web.Http;
using HMS.Web.Auth.JwtConfig;

namespace HMS.Web.Controllers.Api
{
    [JwtAuthorize]
    public class MyProtectedApiController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok(new
            {
                Id = 1,
                Title = "Hello from My Protected Controller!",
                Username = this.User.Identity.Name
            });
        }
    }
}