using BudgetHelper.Views;

namespace BudgetHelper
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Views.MainPage),typeof(Views.MainPage)); 
            Routing.RegisterRoute(nameof(Views.AddExpense), typeof(Views.AddExpense));
            Routing.RegisterRoute(nameof(Views.ExpensePage), typeof(Views.ExpensePage));
            Routing.RegisterRoute(nameof(Views.CategoryDetailsPage), typeof(Views.CategoryDetailsPage));

        }
        
       
    }
}
