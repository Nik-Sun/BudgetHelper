using BudgetHelper.Core;
using Microsoft.EntityFrameworkCore;

namespace BudgetHelper.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly ApplicationDbContext ctx;
        int count = 0;
       
        public MainPage(ApplicationDbContext ctx)
        {
            InitializeComponent();
            this.ctx = ctx;
            
        }
        
        private async void OnCounterClicked(object sender, EventArgs e)
        {

          





            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
