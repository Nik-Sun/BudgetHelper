using BudgetHelper.Core.Entities;
using Microsoft.Maui.Animations;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetHelper.Core
{
    public class BudgetHelperDb
    {
        private SQLiteAsyncConnection database;
        public BudgetHelperDb()
        {
           // database = new SQLiteAsyncConnection(Constants.DatabasePath,Constants.Flags); 
        }

        public async Task Init()
        {
            if (database != null)
            {
                return;
            }
            database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await database.ExecuteAsync(" PRAGMA foreign_keys = ON;");
            var categoryResult = await database.CreateTableAsync<Category>();
            var expenseResult = await database.CreateTableAsync<ExpenseType>();
            _ = await database.CreateTableAsync<Expense>();

            if (categoryResult == CreateTableResult.Created)
            {
                await database.InsertAllAsync(InitialSeed.SeedCategories());
            }
            if (expenseResult == CreateTableResult.Created)
            {
                await database.InsertAllAsync(InitialSeed.SeedExpenseTypes());
            }
          
            
        }

        public async Task<List<ExpenseType>> GetItemsAsync()
        {
            await Init();
            
            var result = await database.Table<ExpenseType>().ToListAsync();
            return result;
        }
    }
}
