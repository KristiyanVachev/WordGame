using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Bytes2you.Validation;
using WordGame.Data.Contracts;
using WordGame.Factories;
using WordGame.Models;
using WordGame.Services.Contracts;

namespace WordGame.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly IUserFactory userFactory;
        private readonly IUnitOfWork unitOfWork;

        public UserService(IRepository<User> userRepository,
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

        public string CreateHash(string password)
        {
            //Calculate MD5 hash from input
            MD5 md5 = MD5.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(inputBytes);

            //Convert byte array to hex 
            StringBuilder sb = new StringBuilder();

            foreach (byte b in hash)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }

        public User Register(string userName, string password, string fullName, string email)
        {
            string passwordHash = this.CreateHash(password);

            User newUser = this.userFactory.Register(userName, passwordHash, fullName, email);

            this.userRepository.Add(newUser);
            this.unitOfWork.Commit();

            return newUser;
        }

        public string Login(string userName, string password)
        {
            string authKey = Guid.NewGuid().ToString();

            throw new System.NotImplementedException();
        }
    }
}
