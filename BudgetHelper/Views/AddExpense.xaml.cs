using BudgetHelper.Core;
using Microsoft.EntityFrameworkCore;

namespace BudgetHelper.Views;

public partial class AddExpense : ContentPage
{
    private readonly ApplicationDbContext ctx;

    public AddExpense(ApplicationDbContext ctx)
    {
        InitializeComponent();
        this.ctx = ctx;

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await PopulateControl();
    }

    private async Task PopulateControl()
    {
        var existing = await ctx.ExpenseTypes.Select(x => x.Name)
            .ToListAsync();
        if (CategoryList.Items.Count > 0)
        {
            CategoryList.Items.Clear();
        }
        foreach (var expenseType in existing)
        {
            CategoryList.Items.Add(expenseType);
        }

        CategoryList.Items.Add(" {Add a new type of expense} ");
        CategoryList.SelectedIndex = 0;
        
    }

    private async void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        var option = (string)picker.SelectedItem;
        if(option == " {Add a new type of expense} ")
        {
            await Shell.Current.GoToAsync("MainPage");
        }
        else
        {
            MainStack.Children.Add(new Entry());
        }
    }

   
}