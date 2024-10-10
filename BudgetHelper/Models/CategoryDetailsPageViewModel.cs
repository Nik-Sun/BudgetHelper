using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetHelper.Models
{
    public class CategoryDetailsPageViewModel
    {
        public CategoryDetailsPageViewModel()
        {
            IsExpenseDeleted = false;
        }
        public bool IsExpenseDeleted { get; set; }
    }
}
