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
        public Task<HttpResponseMessage> Index([FromBody] User newUser)
        {
            //TODO create a separate model for request, here passwordHash is the normal password

            //check if user exists, if it does return something else

            var registeredUser = this.userService.Register(
                newUser.UserName, 
                newUser.PasswordHash,
                newUser.FullName,
                newUser.Email
                );

            if (registeredUser != null)
            {
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.Created));

            }
            else
            {
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.Conflict));
            }



        }

        [HttpGet]
        public Task<HttpResponseMessage> Get(int id)
        {
            return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK));
        }
    }
}