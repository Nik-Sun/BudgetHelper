using BudgetHelper.Core;
using BudgetHelper.Core.Entities;
using BudgetHelper.Models;
using CommunityToolkit.Maui.Behaviors;
using DevExpress.Maui.Core.Internal;
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
            Id = et.Id,
            Name = et.Name,
            TypeId = et.Id,
            Value = 0

        })
            .OrderBy(e => e.Name)
            .ToListAsync();

        existing.Add(new ExpenseViewModel()
        {
            Name = MessagesConstants.AddNewTypeCustomName
        });
        ExpenseList.ItemsSource = existing;

        if (!ExpenseList.Behaviors.OfType<TouchBehavior>().Any())
        {
            var behaviour = new TouchBehavior
            {
                LongPressCommand = new Command(async (object sender) =>
                {
                    var picker = sender as Picker;
                    var selected = (ExpenseViewModel)picker.SelectedItem;

                    string action = await DisplayActionSheet(MessagesConstants.TypePickerActionSheetTitle
                        ,MessagesConstants.PopUpClose
                        ,null,
                        MessagesConstants.ActionTypeDelete,
                        MessagesConstants.ActionTypeCheck);

                    if (action == MessagesConstants.ActionTypeDelete)
                    {
                        bool answer = await DisplayAlert(MessagesConstants.DeleteAlertTitle,
                            string.Format(MessagesConstants.DeleteConfirmMessage,selected.Name),
                            MessagesConstants.ActionTypeDelete, 
                            MessagesConstants.PopUpClose);
                        if (answer)
                        {
                            await DeleteExpenseType(selected.Id);

                            await DisplayAlert(MessagesConstants.DeletedTitle,
                               string.Format(MessagesConstants.DeletedMessage,selected.Name),
                                MessagesConstants.PopUpClose);
                        }
                    }
                    else if(action == MessagesConstants.ActionTypeCheck)
                    {
                        var parameter = new Dictionary<string, object>
                        {
                           { "Id",selected.Id },
                        };
                        await Shell.Current.GoToAsync($"{nameof(CategoryDetailsPage)}",parameter);
                    }
                   

                }),
                PressedBackgroundColor = Colors.Transparent,
                PressedScale = 0.95,
                LongPressCommandParameter = ExpenseList

            };
            ExpenseList.Behaviors.Add(behaviour);
       }
        
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
        await SaveNewExpense();
    }


    private async Task DeleteExpenseType(int id)
    {
        var expenseType = ctx.ExpenseTypes.Find(id);
        if (expenseType != null)
        {
            ctx.ExpenseTypes.Remove(expenseType);
            await ctx.SaveChangesAsync();
            await PopulateControl();
        }

    }

    private async Task SaveNewExpense()
    {
        string value = ExpenseValue.Text;
        var selected = (ExpenseViewModel)ExpenseList.SelectedItem;
        var dateCreated = ExpenseDate.Date;
        if (decimal.TryParse(value, out decimal result) && result > 0)
        {

            var expense = new Expense()
            {
                Value = result,
                Created = dateCreated,
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



        }
        else
        {
            await DisplayAlert("Invalid input", $"'{value}' не е валидно число!", "Разбрах");
        }
    }
}