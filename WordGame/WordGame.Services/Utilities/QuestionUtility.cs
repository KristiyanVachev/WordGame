﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Bytes2you.Validation;
//using Microsoft.AspNet.Identity;
//using WordGame.Data.Contracts;
//using WordGame.Factories;
//using WordGame.Models;
//using WordGame.Services.Utilities.Contracts;

//namespace WordGame.Services.Utilities
//{
//    public class QuestionUtility : IQuestionUtility
//    {
//        private readonly IRepository<Post> questionRepository;
//        private readonly IRepository<Category> categoryRepository;
//        private readonly IQuestionFactory questionFactory;
//        private readonly IUnitOfWork unitOfWork;

//        public QuestionUtility(IRepository<Post> questionRepository,
//            IRepository<Category> categoryRepository,
//            IQuestionFactory questionFactory,
//            IUnitOfWork unitOfWork)
//        {
//            Guard.WhenArgument(questionRepository, "questionRepository cannot be null").IsNull().Throw();
//            Guard.WhenArgument(categoryRepository, "categoryRepository cannot be null").IsNull().Throw();
//            Guard.WhenArgument(questionFactory, "questionFactory cannot be null").IsNull().Throw();
//            Guard.WhenArgument(unitOfWork, "unitOfWork cannot be null").IsNull().Throw();

//            this.questionRepository = questionRepository;
//            this.categoryRepository = categoryRepository;
//            this.questionFactory = questionFactory;
//            this.unitOfWork = unitOfWork;
//        }

//        public IEnumerable<Post> GetQuestions()
//        {
//            var questions = new List<Post>();

//            var categoryIds = this.categoryRepository.Entities.Select(x => x.Id).ToList();

//            foreach (var categoryId in categoryIds)
//            {
//                //TODO: Optimization: Avoid sorting all the questions by getting all the needed question's Id's and then getting 3 random Id's
//                var categoryQuestions = this.questionRepository.Entities
//                    .Where(x => x.CategoryId == categoryId)
//                    .OrderBy(x => Guid.NewGuid());

//                foreach (var categoryQuestion in categoryQuestions)
//                {
//                    questions.Add(categoryQuestion);
//                }
//            }

//            return questions;
//        }

//        public Post CreateQuestion(Submission submission)
//        {
//            var newQuestion = this.questionFactory.CreateQuestion(submission.Condition);

//            //Category
//            var category = categoryRepository.Entities.FirstOrDefault(x => x.Name == submission.Category);
//            newQuestion.Category = category;

//            //Answers
//            var answers = new List<Answer>();
//            answers.Add(this.questionFactory.CreateAnswer(submission.CorrectAnswer, true));

//            foreach (var incorrectAnswer in submission.IncorrectAnswers)
//            {
//                answers.Add(this.questionFactory.CreateAnswer(incorrectAnswer.Content, false));
//            }

//            newQuestion.Answers = answers;

//            //TODO: ASK adding the answers to answerRepository?

//            this.questionRepository.Add(newQuestion);
//            this.unitOfWork.Commit();

//            return newQuestion;
//        }

//        public Post GetById(int id)
//        {
//            return this.questionRepository.GetById(id);
//        }
//    }
//}
