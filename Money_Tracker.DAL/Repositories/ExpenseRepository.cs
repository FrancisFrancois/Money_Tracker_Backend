using Money_Tracker.DAL.Entities;
using Money_Tracker.DAL.Interfaces;
using Money_Tracker.DAL.Mappers;
using Money_Tracker.Tools.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.DAL.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly DbConnection _DbConnection;

        public ExpenseRepository(DbConnection dbConnection)
        {
            _DbConnection = dbConnection;
        }

        public IEnumerable<Expense> GetAll()
        {
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM [Expense]";
                _DbConnection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return ExpenseMapper.Mapper(reader);
                    }
                };
                _DbConnection.Close();
            }
        }

        public Expense? GetById(int id)
        {
            Expense? result = null;
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM [Expense] WHERE [Expense_Id] = @id";
                command.addParamWithValue("id", id);
                _DbConnection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = ExpenseMapper.Mapper(reader);
                    }
                };
                _DbConnection.Close();
            };
            return result;
        }
        public Expense Create(Expense expense)
        {
            Expense result;
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                command.CommandText = "INSERT INTO [Expense] ([Amount], [Description], [Date_Expense]) " +
                                      " OUTPUT INSERTED.* " +
                                      "VALUES (@amount, @description, @date_expense)";
                command.addParamWithValue("amount", expense.Amount);
                command.addParamWithValue("description", expense.Description);
                command.addParamWithValue("date", expense.Date_Expense);
                _DbConnection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new Exception("Failed to create expense");
                    }
                    result = ExpenseMapper.Mapper(reader);
                };
                _DbConnection.Close();
            };
            return result;
        }

        public bool Update(int id, Expense expense)
        {
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                command.CommandText = "UPDATE [Expense] " +
                                      "SET [Amount] = @amount, " +
                                      "[Description] = @description, " +
                                      "[Date_Expense] = @date_expense " +
                                      "WHERE [Expense_Id] = @id";
                command.addParamWithValue("amount", expense.Amount);
                command.addParamWithValue("description", expense.Description);
                command.addParamWithValue("date_expense", expense.Date_Expense);
                command.addParamWithValue("id", id);
                _DbConnection.Open();
                int nbRowUpdated = command.ExecuteNonQuery();
                _DbConnection.Close();
                return nbRowUpdated == 1;

            }
        }

        public bool Delete(int id)
        {
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                command.CommandText = "DELETE FROM [Expense] WHERE [Expense_Id] = @id";
                command.addParamWithValue("id", id);
                _DbConnection.Open();
                int nbRowDeleted = command.ExecuteNonQuery();
                _DbConnection.Close();
                return nbRowDeleted == 1;
            };
        }

        public IEnumerable<Expense> GetExpensesByDay(DateTime date)
        {
            List<Expense> expenses = new List<Expense>();
            try
            {
                using (DbCommand command = _DbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [Expense] WHERE CAST([Date_Expense] AS DATE) = @date";
                    command.addParamWithValue("date", date.Date);
                    _DbConnection.Open();
                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            expenses.Add(ExpenseMapper.Mapper(reader));
                        }
                    }
                    _DbConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to list Expense By Day", ex);
            }
            return expenses;
        }

        public IEnumerable<Expense> GetExpensesByWeek(DateTime date)
        {
            List<Expense> expenses = new List<Expense>();
            try
            {
                DayOfWeek firstDayOfWeek = DayOfWeek.Monday; 
                int daysUntilFirstDayOfWeek = (7 + date.DayOfWeek - firstDayOfWeek) % 7;
                DateTime startDate = date.AddDays(-daysUntilFirstDayOfWeek);
                DateTime endDate = startDate.AddDays(7);

                using (DbCommand command = _DbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [Expense] WHERE [Date_Expense] >= @startDate AND [Date_Expense] < @endDate";
                    command.addParamWithValue("startDate", startDate.Date);
                    command.addParamWithValue("endDate", endDate.Date);
                    _DbConnection.Open();
                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            expenses.Add(ExpenseMapper.Mapper(reader));
                        }
                    }
                    _DbConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to list Expenses By Week", ex);
            }
            return expenses;
        }

        public IEnumerable<Expense> GetExpensesByMonth(DateTime date)
        {
            List<Expense> expenses = new List<Expense>();
            try
            {
                using (DbCommand command = _DbConnection.CreateCommand())
                {
                    DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                    DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                    command.CommandText = "SELECT * FROM [Expense] WHERE [Date_Expense] >= @startDate AND [Date_Expense] <= @endDate";
                    command.addParamWithValue("startDate", firstDayOfMonth);
                    command.addParamWithValue("endDate", lastDayOfMonth);
                    _DbConnection.Open();

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            expenses.Add(ExpenseMapper.Mapper(reader));
                        }
                    }
                    _DbConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to list Expenses By Month", ex);
            }

            return expenses;
        }
        public IEnumerable<Expense> GetExpensesByYear(DateTime date)
        {
            List<Expense> expenses = new List<Expense>();
            try
            {
                using (DbCommand command = _DbConnection.CreateCommand())
                {
                    DateTime firstDayOfYear = new DateTime(date.Year, 1, 1);
                    DateTime lastDayOfYear = new DateTime(date.Year, 12, 31);

                    command.CommandText = "SELECT * FROM [Expense] WHERE [Date_Expense] >= @startDate AND [Date_Expense] <= @endDate";
                    command.addParamWithValue("startDate", firstDayOfYear);
                    command.addParamWithValue("endDate", lastDayOfYear);
                    _DbConnection.Open();

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            expenses.Add(ExpenseMapper.Mapper(reader));
                        }
                    }
                    _DbConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to list Expenses By Year", ex);
            }

            return expenses;
        }

        public double GetTotalExpensesByDay(DateTime date)
        {
            double total = 0;
            try
            {
                using (DbCommand command = _DbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT SUM(Amount) FROM [Expense] WHERE CAST([Date_Expense] AS DATE) = @date";
                    command.addParamWithValue("date", date.Date);

                    _DbConnection.Open();
                    var result = command.ExecuteScalar();
                    total = (result != DBNull.Value) ? Convert.ToDouble(result) : 0;
                    _DbConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to calculate total Expense By Day", ex);
            }
            return total;
        }

        public double GetTotalExpensesByWeek(DateTime date)
        {
            double total = 0;
            try
            {
                DayOfWeek firstDayOfWeek = DayOfWeek.Monday;
                int daysUntilFirstDayOfWeek = (7 + date.DayOfWeek - firstDayOfWeek) % 7;
                DateTime startDate = date.AddDays(-daysUntilFirstDayOfWeek);
                DateTime endDate = startDate.AddDays(7);


                using (DbCommand command = _DbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT SUM(Amount) FROM [Expense] WHERE [Date_Expense] >= @startDate AND [Date_Expense] < @endDate";
                    command.addParamWithValue("startDate", startDate.Date);
                    command.addParamWithValue("endDate", endDate.Date);

                    _DbConnection.Open();
                    var result = command.ExecuteScalar();
                    total = (result != DBNull.Value) ? Convert.ToDouble(result) : 0;
                    _DbConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to calculate total Expenses By Week", ex);
            }
            return total;
        }

        public double GetTotalExpensesByMonth(DateTime date)
        {
            double total = 0;
            try
            {
                DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                using (DbCommand command = _DbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT SUM(Amount) FROM [Expense] WHERE [Date_Expense] >= @startDate AND [Date_Expense] <= @endDate";
                    command.addParamWithValue("startDate", firstDayOfMonth);
                    command.addParamWithValue("endDate", lastDayOfMonth);

                    _DbConnection.Open();
                    var result = command.ExecuteScalar();
                    total = (result != DBNull.Value) ? Convert.ToDouble(result) : 0;
                    _DbConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to calculate total Expenses By Month", ex);
            }

            return total;
        }

        public double GetTotalExpensesByYear(DateTime date)
        {
            double total = 0;
            try
            {
                DateTime firstDayOfYear = new DateTime(date.Year, 1, 1);
                DateTime lastDayOfYear = new DateTime(date.Year, 12, 31);

                using (DbCommand command = _DbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT SUM(Amount) FROM [Expense] WHERE [Date_Expense] >= @startDate AND [Date_Expense] <= @endDate";
                    command.addParamWithValue("startDate", firstDayOfYear);
                    command.addParamWithValue("endDate", lastDayOfYear);

                    _DbConnection.Open();
                    var result = command.ExecuteScalar();
                    total = (result != DBNull.Value) ? Convert.ToDouble(result) : 0;
                    _DbConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to calculate total Expenses By Year", ex);
            }

            return total;
        }

    }
}
