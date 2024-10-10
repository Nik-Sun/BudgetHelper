using BudgetHelper.Core;
using Microsoft.EntityFrameworkCore;

namespace BudgetHelper.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly ApplicationDbContext ctx;
       
        public MainPage(ApplicationDbContext ctx)
        {
            InitializeComponent();
            this.ctx = ctx;
            
        }
        
    }

}
