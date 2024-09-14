using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetHelper.Core.Entities
{
    public class ExpenseType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Expense Expense { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
