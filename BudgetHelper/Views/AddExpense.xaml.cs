using BudgetHelper.Core;
using BudgetHelper.Core.Entities;
using BudgetHelper.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;

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
        //var existing = await ctx.ExpenseTypes.Select(x => x.Name)
        //    .ToListAsync();
        var existeng = await ctx.ExpenseTypes.Select(et => new ExpenseViewModel()
        {
            Name = et.Name,
            TypeId = et.Id,
            Value = 0

        }).ToListAsync();
        existeng.Add(new ExpenseViewModel()
        {
            Name = " {Add a new type of expense} "
        });
        if (ExpenseList.Items.Count > 0)
        {
            ExpenseList.Items.Clear(); 
            ExpenseList.ItemsSource.Clear();
        }
        ExpenseList.ItemsSource = existeng;
        ExpenseList.ItemDisplayBinding = new Binding("Name");
        //foreach (var expenseType in existeng)
        //{
        //    ExpenseList.Items.Add(expenseType.Name);
        //}

        //ExpenseList.Items.Add(" {Add a new type of expense} ");
        // CategoryList.SelectedIndex = 0;

    }

    private async void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        var option = (ExpenseViewModel)picker.SelectedItem;
        if (option.Name == " {Add a new type of expense} ")
        {
            await Shell.Current.GoToAsync("MainPage");
        }
    }

    private void SaveButtonClicked(object sender, EventArgs e)
    {
        string value = ExpenseValue.Text;
        var selected = (ExpenseViewModel)ExpenseList.SelectedItem;

        if (decimal.TryParse(value, out decimal result) && result > 0)
        {
            var expense = new Expense()
            {
                Value = result,
                Created = DateTime.UtcNow,
                TypeId = selected.TypeId
            };

            
        }
    }
}