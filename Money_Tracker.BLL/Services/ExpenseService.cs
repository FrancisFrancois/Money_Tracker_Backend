using Money_Tracker.BLL.Interfaces;
using Money_Tracker.BLL.Mappers;
using Money_Tracker.BLL.Models;
using Money_Tracker.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.BLL.Services
{
    public class ExpenseService : IExpenseService   
    {
        private readonly IExpenseRepository _ExpenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _ExpenseRepository = expenseRepository;
        }
        public IEnumerable<Expense> GetAll()
        {
           return _ExpenseRepository.GetAll().Select(e => e.ToModel());
        }

        public Expense? GetById(int id)
        {
            return _ExpenseRepository.GetById(id)?.ToModel();
        }

        public Expense Create(Expense expense)
        {
            return _ExpenseRepository.Create(expense.ToEntity()).ToModel();
        }

        public bool Update(int id, Expense expense)
        {
            bool updated = _ExpenseRepository.Update(id, expense.ToEntity());
            if (!updated)
            {
                throw new DirectoryNotFoundException("Expense not found");
            }
            return updated;
        }

        public bool Delete(int id)
        {
            bool deleted = _ExpenseRepository.Delete(id);
            if (!deleted)
            {
                throw new DirectoryNotFoundException("Expense not found");
            }
            return deleted;
        }

        public IEnumerable<Expense> GetExpensesByDay(DateTime date)
        {
            return _ExpenseRepository.GetExpensesByDay(date).Select(e => e.ToModel());
        }

        public IEnumerable<Expense> GetExpensesByWeek(DateTime date)
        {
            return _ExpenseRepository.GetExpensesByWeek(date).Select(e => e.ToModel());
        }

        public IEnumerable<Expense> GetExpensesByMonth(DateTime date)
        {
            return _ExpenseRepository.GetExpensesByMonth(date).Select(e => e.ToModel());
        }

        public IEnumerable<Expense> GetExpensesByYear(DateTime date)
        {
            return _ExpenseRepository.GetExpensesByYear(date).Select(e => e.ToModel());
        }
    }
}
