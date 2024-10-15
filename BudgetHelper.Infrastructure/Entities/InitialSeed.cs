using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetHelper.Core.Entities
{
    internal static class InitialSeed
    {
        private static Category SportCategory = new Category { Id = 1, Name = "Домашни любимци" };
        private static Category ComunalCategory = new Category { Id = 2, Name = "Битови сметки" };
        private static Category FunCategory = new Category { Id = 3, Name = "Спорт" };
        private static Category ShoppingCategory = new Category { Id = 4, Name = "Домашни потреби" };
        private static Category GroceriesCategory = new Category { Id = 5, Name = "Хранителни стоки" };
        private static Category RestourantsCategory = new Category { Id = 6, Name = "Ресторанти и развлечения" };
        private static Category HealthCategory = new Category { Id = 7, Name = "Здраве" };
        private static Category BeautyCategory = new Category { Id = 8, Name = "Красота" };
        private static Category CarsCategory = new Category { Id = 9, Name = "Авто разходи" };
        private static Category PresentsCategory = new Category { Id = 10, Name = "Подаръци" };
        private static Category OthersCategory = new Category { Id = 11, Name = "Други разходи" };


        private static ExpenseType Electricity = new ExpenseType { Id = 1, Name = "Електричество", CategoryId = 2 };
        private static ExpenseType Water = new ExpenseType { Id = 2, Name = "Вода", CategoryId = 2 };
        private static ExpenseType Phones = new ExpenseType { Id = 3, Name = "Телефони", CategoryId = 2 };
        private static ExpenseType INet = new ExpenseType { Id = 4, Name = "Интернет и ТВ", CategoryId = 2 };
        private static ExpenseType Spotyfy = new ExpenseType { Id = 5, Name = "Spotify", CategoryId = 2 };
        private static ExpenseType TEC = new ExpenseType { Id = 6, Name = "Топлофикация", CategoryId = 2 };
        private static ExpenseType BuildingExp = new ExpenseType { Id = 7, Name = "Такса вход", CategoryId = 2 };
        private static ExpenseType Food = new ExpenseType { Id = 8, Name = "Lidl", CategoryId = 5 };
        private static ExpenseType Food2 = new ExpenseType { Id = 9, Name = "Kaufland", CategoryId = 5 };
        private static ExpenseType Food3 = new ExpenseType { Id = 10, Name = "Фантастико", CategoryId = 5 };
        private static ExpenseType Food4 = new ExpenseType { Id = 11, Name = "ЦБА", CategoryId = 5 };

        public static IEnumerable<Category> SeedCategories()
        {
            return new Category[] { SportCategory, ComunalCategory, FunCategory, ShoppingCategory, GroceriesCategory
            ,RestourantsCategory,HealthCategory,BeautyCategory,CarsCategory,PresentsCategory,OthersCategory};
        }

        public static IEnumerable<ExpenseType> SeedExpenseTypes()
        {
            return new ExpenseType[] { Electricity, Water, Phones, INet,Spotyfy,TEC,BuildingExp, Food,Food2,Food3,Food4 };
        }
    }
}
