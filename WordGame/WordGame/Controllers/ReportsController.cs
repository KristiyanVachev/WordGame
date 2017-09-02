using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WordGame.Models;
using WordGame.Models.Reports;
using WordGame.Models.Threads;
using WordGame.Models.Threads.Responses;
using WordGame.Services.Contracts;

namespace WordGame.Controllers
{
    public class ReportsController : ApiController
    {
        private IReportService reportService;

        public ReportsController(IReportService reportService)
        {
            if (reportService == null)
            {
                throw new ArgumentNullException("reportService");
            }

            this.reportService = reportService;
        }

        [Route("api/reports")]
        [HttpPost]
        public Task<HttpResponseMessage> Index([FromBody] NewReport newReport)
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

            try
            {
                this.reportService.Create(authKey, newReport.WordId);
            }
            catch (InstanceNotFoundException)
            {
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.Unauthorized));
            }

            return Task.FromResult(Request.CreateResponse(HttpStatusCode.Created));
        }
    }
}