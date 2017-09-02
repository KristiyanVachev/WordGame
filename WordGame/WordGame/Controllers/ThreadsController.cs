using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Channels;
using System.Threading.Tasks;
using System.Web.Http;
using WordGame.Models;
using WordGame.Models.Threads;
using WordGame.Models.Threads.Responses;
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

            ThreadResponse response = new ThreadResponse
            {
                Id = thread.Id,
                Name = thread.Name,
                WordCount = thread.Posts.Count,
                Word = thread.Posts.FirstOrDefault().Word,
                Author = thread.User.UserName
            };

            return Task.FromResult(Request.CreateResponse(HttpStatusCode.Created, response));
        }

        [Route("api/threads/{id}/words")]
        [HttpPost]
        public Task<HttpResponseMessage> Words(int id, [FromBody] NewPost newPost)
        {
            IEnumerable<string> headerValues;

            try
            {
                headerValues = Request.Headers.GetValues("AuthKey");

            }
            catch (Exception)
            {
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.Unauthorized));
            }

            var authKey = headerValues.FirstOrDefault();


            Post created;
            try
            {
               created = this.threadService.AddWord(authKey, id, newPost.Word);

            }
            catch (Exception)
            {
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.Forbidden));
            }

            return Task.FromResult(Request.CreateResponse(HttpStatusCode.Created, created.Word));
        }
    }
}