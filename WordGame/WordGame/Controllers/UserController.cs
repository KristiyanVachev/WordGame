using System;
using System.Linq;
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
            return userService.GetAll().FirstOrDefault().UserName;
        }
    }
}