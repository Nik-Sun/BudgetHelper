using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetHelper.Infrastructure
{
    public static class Helpers
    {
        public static double CalculateFontSize(int textLength)
        {
            int divResult = textLength / 16;
            double factor = Math.Abs(divResult * 10.0 / 100 - 1);

            var fontSize = 16 * factor;
            return fontSize;
        }
    }
}
