using BudgetHelper.Core;
using Microsoft.EntityFrameworkCore;

namespace BudgetHelper.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly ApplicationDbContext ctx;
        int count = 0;
        //public MainPage()
        //{
        //    InitializeComponent();
        //}
        public MainPage(ApplicationDbContext ctx)
        {
            InitializeComponent();
            this.ctx = ctx;
            
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {

            //var text = await ctx.Categories
            //    .Select(c => c.Name)
            //    .ToListAsync();
            //TestLabel.Text = string.Join(" ,", text);

           
            //count++;

            //if (count == 1)
            //{
            //    var itemsCount =  await ctx.GetCountAsync(); 
            //    CounterBtn.Text = $"Clicked {itemsCount} time";

            //}

            //else
            //{
            //    CounterBtn.Text = $"Clicked {count} times";
            //    await ctx.SaveItemAsync(new ToDoItem() {Value = $"{count}"});
            //}
                


            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
