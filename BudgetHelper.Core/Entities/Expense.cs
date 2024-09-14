using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetHelper.Core.Entities
{
    public class Expense
    {
        public int Id { get; set; }

        public int TypeId { get; set; }
        public ExpenseType Type { get; set; }
        public decimal Value { get; set; }
        public DateTime? Created { get; set; }
    }
}
