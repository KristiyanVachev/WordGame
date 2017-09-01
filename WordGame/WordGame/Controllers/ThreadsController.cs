using System;
using System.Management.Instrumentation;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Channels;
using System.Threading.Tasks;
using System.Web.Http;
using WordGame.Models;
using WordGame.Models.Threads;
using WordGame.Services.Contracts;

namespace WordGame.Controllers
{
    public class ThreadsController : ApiController
    {
        private IThreadService threadService;

        public ThreadsController(IThreadService threadService)
        {
            if (threadService == null)
            {
                throw new ArgumentNullException("Thread Service");
            }

            this.threadService = threadService;
        }

        [Route("api/threads")]
        [HttpPost]
        public Task<HttpResponseMessage> Index([FromBody] NewThread newThread)
        {
            Thread thread;

            try
            {
                thread = this.threadService.Create(newThread.AuthKey, newThread.Name, newThread.FirstWord);
            }
            catch (InstanceNotFoundException)
            {
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.Unauthorized));
            }

            return Task.FromResult(Request.CreateResponse(HttpStatusCode.Created, thread));
        }
    }
}