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
        [HttpPost]
        public Task<HttpResponseMessage> Index([FromBody] NewUser newUserInfo)
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

        //[HttpPost]
        //public Task<HttpResponseMessage> Login([FromBody] User newUser)
        //{
                
        //}

        [HttpGet]
        public Task<HttpResponseMessage> Get(int id)
        {
            return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK));
        }
    }
}