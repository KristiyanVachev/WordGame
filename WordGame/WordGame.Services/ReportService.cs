using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using Bytes2you.Validation;
using WordGame.Data.Contracts;
using WordGame.Factories;
using WordGame.Models;
using WordGame.Services.Contracts;

namespace WordGame.Services
{
    public class ReportService : IReportService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Report> reportRepository;
        private readonly IRepository<Post> postRepository;
        private readonly IReportFactory reportFactory;
        private readonly IUnitOfWork unitOfWork;

        public ReportService(IRepository<User> userRepository,
            IRepository<Report> reportRepository,
            IRepository<Post> postRepository,
            IReportFactory reportFactory,
            IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(userRepository, "userRepository cannot be null").IsNull().Throw();
            Guard.WhenArgument(reportRepository, "reportRepository cannot be null").IsNull().Throw();
            Guard.WhenArgument(postRepository, "postRepository cannot be null").IsNull().Throw();
            Guard.WhenArgument(reportFactory, "reportFactory cannot be null").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork cannot be null").IsNull().Throw();

            this.userRepository = userRepository;
            this.reportRepository = reportRepository;
            this.postRepository = postRepository;
            this.reportFactory = reportFactory;
            this.unitOfWork = unitOfWork;
        }


        public Report Create(string authKey, int postId)
        {
            var user = this.userRepository.Entities.FirstOrDefault(x => x.AuthKey == authKey);

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var post = this.postRepository.GetById(postId);

            if (post == null)
            {
                throw new InstanceNotFoundException();
            }

            var report = this.reportFactory.CreateReport(user.Id, post.Id);

            this.reportRepository.Add(report);
            this.unitOfWork.Commit();

            return report;
        }

        public IEnumerable<Report> Reports(string authKey)
        {
            var user = this.userRepository.Entities.FirstOrDefault(x => x.AuthKey == authKey);

            if (user == null || !user.IsAdmin)
            {
                throw new UnauthorizedAccessException();
            }

            var reports = this.reportRepository.Entities.ToList();

            return reports;
        }

        public void Process(string authKey, int reportId)
        {
            throw new NotImplementedException();
        }
    }
}
