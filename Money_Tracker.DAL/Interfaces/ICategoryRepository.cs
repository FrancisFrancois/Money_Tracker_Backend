﻿using Money_Tracker.DAL.Entities;
using Money_Tracker.Tools.Interfaces;

namespace Money_Tracker.DAL.Interfaces
{
    public interface ICategoryRepository : ICrud<int, Category>
    {
    }
}