using System;
using System.Web.Http;
using WordGame.Services.Contracts;

namespace WordGame.Controllers
{
    public class UserController : ApiController
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            if (userService == null)
            {
                throw new ArgumentNullException("User Service");
            }

            this.userService = userService;
        }

        // GET api/user
        public string Get()
        {
            return userService.Register("dolphin", "gupa", "Dolphin Fin", "fun@sea.oc").UserName;
            
        }
    }
}