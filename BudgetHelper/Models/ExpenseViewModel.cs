﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetHelper.Models
{
    public class ExpenseViewModel
    {
        public string Name { get; set; }
        public int TypeId { get; set; }
        public decimal Value { get; set; }
    }
}
