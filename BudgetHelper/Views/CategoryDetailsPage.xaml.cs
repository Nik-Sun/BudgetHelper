using BudgetHelper.Core;
using BudgetHelper.Core.Entities;
using BudgetHelper.Infrastructure;
using BudgetHelper.Models;
using CommunityToolkit.Maui.Behaviors;
using DevExpress.Maui.Core.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls.Shapes;
using System.Globalization;

namespace BudgetHelper.Views;

public partial class CategoryDetailsPage : ContentPage, IQueryAttributable
{
    private readonly ApplicationDbContext ctx;

    private string date;
    private string categoryName;

    private string typeName;
    private int? typeId;

    private List<ExpenseDetailModel> expenses;

    public CategoryDetailsPage(ApplicationDbContext ctx)
    {
        InitializeComponent();
        this.ctx = ctx;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("Date"))
        {
            date = (string)query["Date"];
        }
        if (query.ContainsKey("Category"))
        {
            categoryName = (string)query["Category"];
        }
        if (query.ContainsKey("Id"))
        {
            typeId = (int)query["Id"];
        }
        if (query.ContainsKey("TypeName"))
        {
            typeName = (string)query["TypeName"];
        }

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        expenses = await GetExpensesAsync();
        ParentStack.Children.Clear();
        CreateLabels(expenses);
    }

    private async Task<List<ExpenseDetailModel>> GetExpensesAsync()
    {
        var result = new List<ExpenseDetailModel>();

        if (typeId != null && typeName != null)
        {
            result = await ctx.Expenses
            .Where(e => e.TypeId == typeId)
            .OrderByDescending(e => e.Created)
            .Select(e => new ExpenseDetailModel
            {
                Id = e.Id,
                Created = e.Created,
                Name = e.Type.Name,
                Value = $"{e.Value:f2}"
            }).ToListAsync();

            this.Title = $"Разходи от тип {typeName}";
        }
        else if (date != null && categoryName != null)
        {
            DateTime actualDate = DateTime.Parse(date,new CultureInfo("BG-bg"));
            result = await ctx.Expenses
                .Where(e => e.Created.Month == actualDate.Month && e.Type.Category.Name == categoryName)
                .OrderByDescending(e => e.Created)
                .Include(e => e.Type)
                .Select(e => new ExpenseDetailModel
                {
                    Id = e.Id,
                    Name = e.Type.Name,
                    Created = e.Created,
                    Value = $"{e.Value:f2}"
                })
                .ToListAsync();

            this.Title = $"Разходи от {categoryName}";
        }

        return result;
    }


    private void CreateLabels(List<ExpenseDetailModel> expenses)
    {
        
        foreach (var expense in expenses)
        {

            var label = new Label
            {
                Text = $"{expense.Created.ToString("d/MM/yy", new CultureInfo("BG-bg"))} : {expense.Name} - {expense.Value}лв",
                FontAttributes = FontAttributes.Bold,
                TextColor = Colors.Black,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(10),
                LineBreakMode = LineBreakMode.MiddleTruncation

            };
            label.FontSize = Helpers.CalculateFontSize(label.Text.Length);
            var frame = new Border
            {
                Content = label,
                BackgroundColor = Colors.AliceBlue,
                Stroke = Colors.DarkGray,
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius(11)
                },
                Padding = new Thickness(5),
                Margin = new Thickness(5),

                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center

            };

            var behaviour = new TouchBehavior
            {
                LongPressCommand = new Command(async (object exp) =>
                {
                    var expense = exp as ExpenseDetailModel;
                    bool answer = await DisplayAlert(
                        MessagesConstants.WarningPopUpTitle,
                        string.Format(MessagesConstants.DeleteExpenseConfirmMessage,expense.Name), 
                        MessagesConstants.PopUpConfirm, 
                        MessagesConstants.PopUpClose);
                    if (answer)
                    {
                        await DeleteExpense(expense.Id);
                    }
                }),
                PressedBackgroundColor = Colors.Transparent,
                PressedScale = 0.95,
                LongPressCommandParameter = expense

            };
            frame.Behaviors.Add(behaviour);
            ParentStack.Children.Add(frame);
        }


    }


    private async Task DeleteExpense(int id)
    {
        var expenseToDelete = ctx.Expenses.Find(id);
        if (expenseToDelete != null)
        {
            ctx.Expenses.Remove(expenseToDelete);
            await ctx.SaveChangesAsync();
        }


        var index = expenses.FindIndex(e => e.Id == id);
        expenses.RemoveAt(index);
        CreateLabels(expenses);
    }
}