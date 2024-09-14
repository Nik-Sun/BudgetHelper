using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetHelper.Core.Entities
{
    internal static class InitialSeed
    {
        //Initial categories,can be expanded later in app.
        private static Category SportCategory = new Category { Id = 1, Name = "Sport" };
        private static Category ComunalCategory = new Category { Id = 2, Name = "Comunal Expenses" };
        private static Category FunCategory = new Category { Id = 3, Name = "Outoors and Fun" };
        private static Category ShoppingCategory = new Category { Id = 4, Name = "Shopping" };
        private static Category GroceriesCategory = new Category { Id = 5, Name = "Groceries" };


        private static ExpenseType Electricity = new ExpenseType { Id = 1, Name = "Electricity", CategoryId = 2 };
        private static ExpenseType Water = new ExpenseType { Id = 2, Name = "Water", CategoryId = 2 };
        private static ExpenseType Phones = new ExpenseType { Id = 3, Name = "Phones", CategoryId = 2 };
        private static ExpenseType INet = new ExpenseType { Id = 4, Name = "Internet and TV", CategoryId = 2 };
        private static ExpenseType Food = new ExpenseType { Id = 5, Name = "Food", CategoryId = 5 };

        public static IEnumerable<Category> SeedCategories()
        {
            return new Category[] { SportCategory, ComunalCategory, FunCategory, ShoppingCategory, GroceriesCategory };
        }

        public static IEnumerable<ExpenseType> SeedExpenseTypes()
        {
            return new ExpenseType[] { Electricity, Water, Phones, INet, Food };
        }
    }
}
