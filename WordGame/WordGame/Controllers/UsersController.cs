using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WordGame.Models;
using WordGame.Services.Contracts;

namespace WordGame.Controllers
{
    public class UsersController : ApiController
    {
        private IUserService userService;

        public UsersController(IUserService userService)
        {
            if (userService == null)
            {
                throw new ArgumentNullException("User Service");
            }

            this.userService = userService;
        }

        // GET api/users
        [Route("api/users/register")]
        [HttpPost]
        public Task<HttpResponseMessage> Register([FromBody] NewUser newUserInfo)
        {
            //TODO check if user exists, if it does return something else

            var user = this.userService.Register(
                newUserInfo.UserName, 
                newUserInfo.Password,
                newUserInfo.FullName,
                newUserInfo.Email
                );

            if (user != null)
            {
                var registeredUser = new RegisteredUser(
                    user.Id,
                    user.UserName,
                    user.PasswordHash,
                    user.FullName,
                    user.Email, 
                    user.IsAdmin
                    );
                
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.Created, registeredUser));

            }
            else
            {
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.Conflict));
            }

        }

        [Route("api/users/login")]
        [HttpPost]
        public Task<HttpResponseMessage> Login([FromBody] Login login)
        {
            string authKey = this.userService.Login(login.Username, login.Password);

            if (authKey != string.Empty)
            {
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.Created, authKey));
            }

            return Task.FromResult(Request.CreateResponse(HttpStatusCode.Forbidden));
        }

        [Route("api/users/logout")]
        [HttpDelete]
        public Task<HttpResponseMessage> Logout([FromBody] Logout logout)
        {
            if (logout == null || logout.AuthKey == string.Empty)
            {
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.BadRequest));
            }

            var result = this.userService.Logout(logout.AuthKey);       
            
            if (result)
            {
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK));
            }

            return Task.FromResult(Request.CreateResponse(HttpStatusCode.NotFound));
        }

        [HttpGet]
        public Task<HttpResponseMessage> Get(int id)
        {
            return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK));
        }
    }
}