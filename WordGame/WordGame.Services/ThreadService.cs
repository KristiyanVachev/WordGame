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
    public class ThreadService : IThreadService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Thread> threadRepository;
        private readonly IRepository<Post> postRepository;
        private readonly IThreadFactory threadFactory;
        private readonly IUnitOfWork unitOfWork;

        public ThreadService(IRepository<User> userRepository,
            IRepository<Thread> threadRepository,
            IRepository<Post> postRepository,
            IThreadFactory threadFactory,
            IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(userRepository, "userRepository cannot be null").IsNull().Throw();
            Guard.WhenArgument(threadRepository, "threadRepository cannot be null").IsNull().Throw();
            Guard.WhenArgument(postRepository, "postRepository cannot be null").IsNull().Throw();
            Guard.WhenArgument(threadFactory, "userFactory cannot be null").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork cannot be null").IsNull().Throw();

            this.userRepository = userRepository;
            this.threadRepository = threadRepository;
            this.postRepository = postRepository;
            this.threadFactory = threadFactory;
            this.unitOfWork = unitOfWork;
        }

        public Thread Create(string authKey, string name, string firstWord)
        {
            var user = this.userRepository.Entities.FirstOrDefault(x => x.AuthKey == authKey);

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var thread = this.threadFactory.CreateThread(user.Id, name);

            var firstPost = this.threadFactory.CreatePost(user.Id, thread.Id, firstWord);

            thread.Posts.Add(firstPost);

            this.threadRepository.Add(thread);
            this.postRepository.Add(firstPost);
            this.unitOfWork.Commit();

            return thread;
        }

        public Post AddWord(string authKey, int threadId, string word)
        {
            var user = this.userRepository.Entities.FirstOrDefault(x => x.AuthKey == authKey);

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var thread = this.threadRepository.GetById(threadId);

            if (thread == null)
            {
                throw new InstanceNotFoundException();
            }

            if (thread.Posts.Any(x => x.Word == word))
            {
                throw new Exception();
            }

            string lastWord = thread.Posts.LastOrDefault().Word;

            if (lastWord[lastWord.Length -1] != word[0])
            {
                throw new ArgumentException();
            }

            //TODO banned word

            var newPost = this.threadFactory.CreatePost(user.Id, threadId, word);

            this.postRepository.Add(newPost);
            this.unitOfWork.Commit();

            return newPost;
        }

        public IEnumerable<Post> Posts(string authKey, int threadId, int skip, int take)
        {
            var user = this.userRepository.Entities.FirstOrDefault(x => x.AuthKey == authKey);

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var thread = threadRepository.GetById(threadId);

            if (thread == null)
            {
                throw new InstanceNotFoundException();
            }

            return thread.Posts.Skip(skip).Take(take).ToList();
        }
    }
}
