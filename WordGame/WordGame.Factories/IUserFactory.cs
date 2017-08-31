﻿using WordGame.Models;

namespace WordGame.Factories
{
    public interface IUserFactory
    {
        CategoryStatistic CreateCategoryStatistic(int categoryId);
    }
}