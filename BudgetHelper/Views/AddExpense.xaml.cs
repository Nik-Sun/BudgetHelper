using BudgetHelper.Core;
using BudgetHelper.Core.Entities;
using BudgetHelper.Models;
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
        
        var existing = await ctx.ExpenseTypes.Select(et => new ExpenseViewModel()
        {
            Name = et.Name,
            TypeId = et.Id,
            Value = 0

        }).ToListAsync();

        existing.Add(new ExpenseViewModel()
        {
            Name = " {Добавете нов тип разход} "
        });
        
        ExpenseList.ItemsSource = existing;
        ExpenseList.ItemDisplayBinding = new Binding("Name");
        ExpenseList.SelectedIndex = 0;
      

    }

    private async void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        var option = (ExpenseViewModel)picker.SelectedItem;
        if (option.TypeId == 0)
        {
            await Shell.Current.GoToAsync($"/{nameof(AddNewTypePage)}");
        }
    }

    private async void SaveButtonClicked(object sender, EventArgs e)
    {
        string value = ExpenseValue.Text;
        var selected = (ExpenseViewModel)ExpenseList.SelectedItem;

        if (decimal.TryParse(value, out decimal result) && result > 0)
        {
            var expense = new Expense()
            {
                Value = result,
                Created = DateTime.Now,
                TypeId = selected.TypeId,
            };
            try
            {
                ctx.Expenses.Add(expense);
                await ctx.SaveChangesAsync();
                ExpenseValue.Text = string.Empty;
                ExpenseList.SelectedIndex = 0;
                await Shell.Current.GoToAsync($"///{nameof(ExpensePage)}");
            }
            catch (Exception ex)
            {

               throw ex.InnerException;
            }

            //TODO: Add optional date
           
        }
        else
        {
            await DisplayAlert("Invalid input",$"'{value}' не е валидно число!","Разбрах");
        }


    }
}