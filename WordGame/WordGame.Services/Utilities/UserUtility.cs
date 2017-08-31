using System.Collections.Generic;
using System.Linq;
using Bytes2you.Validation;
using WordGame.Data.Contracts;
using WordGame.Factories;
using WordGame.Models;
using WordGame.Services.Utilities.Contracts;

namespace WordGame.Services.Utilities
{
    public class UserUtility : IUserUtility
    {
        private readonly IRepository<User> userRepository;
        private readonly IUserFactory userFactory;
        private readonly IUnitOfWork unitOfWork;

        public UserUtility(IRepository<User> userRepository,
            IUserFactory userFactory,
            IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(userRepository, "userRepository cannot be null").IsNull().Throw();
            Guard.WhenArgument(userFactory, "userFactory cannot be null").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork cannot be null").IsNull().Throw();

            this.userRepository = userRepository;
            this.userFactory = userFactory;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<User> GetAll()
        {
            return this.userRepository.Entities.ToList();
        }

        public User GetById(string id)
        {
            return this.userRepository.GetById(id);
        }
    }
}
