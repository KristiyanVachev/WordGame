using System;
using System.Web.Http;
using WordGame.Services.Utilities.Contracts;

namespace WordGame.Controllers
{
    public class UserController : ApiController
    {
        private IUserUtility userService;

        public UserController(IUserUtility userService)
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
            return userService.GetById("4d2f5c56-0c12-4602-a41c-556f51d4079e").Email;
        }
    }
}