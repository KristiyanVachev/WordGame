﻿using System;
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

            if (userRepository.Entities.Any(x => x.UserName == userName))
            {
                throw new ArgumentException();
            }

            User newUser = this.userFactory.Register(userName, passwordHash, fullName, email);

            if (!userRepository.Entities.Any())
            {
                newUser.IsAdmin = true;
            }

            this.userRepository.Add(newUser);
            this.unitOfWork.Commit();

            return newUser;
        }

        public string Login(string userName, string password)
        {
            var user = this.userRepository.Entities.FirstOrDefault(x => x.UserName == userName);

            //If user exists
            if (user == null)
            {
                //TODO not user found
                return string.Empty;
            }

            //Password is correct
            var loginHash = this.CreateHash(password);

            if (user.PasswordHash == loginHash)
            {
                var authKey = Guid.NewGuid().ToString();

                user.AuthKey = authKey;

                this.userRepository.Update(user);
                this.unitOfWork.Commit();

                return authKey;
            }

            //Incorrect password
            return string.Empty;
        }

        public bool Logout(string authKey)
        {
            var user = this.userRepository.Entities.FirstOrDefault(x => x.AuthKey == authKey);

            if (user == null)
            {
                return false;
            }

            user.AuthKey = string.Empty;
            this.userRepository.Update(user);
            this.unitOfWork.Commit();

            return true;
        }

        public bool Delete(string authKey, string password)
        {
            var user = this.userRepository.Entities.FirstOrDefault(x => x.AuthKey == authKey);

            if (user == null)
            {
                return false;
            }

            var passwordHash = this.CreateHash(password);
            if (passwordHash != user.PasswordHash)
            {
                return false;
            }

            //TODO delete everything else
            this.userRepository.Delete(user);
            this.unitOfWork.Commit();

            return true;
        }
    }
}
