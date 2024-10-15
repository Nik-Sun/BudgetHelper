using BudgetHelper.Core;
using BudgetHelper.Core.Entities;
using BudgetHelper.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

namespace BudgetHelper.Views;

public partial class AddNewTypePage : ContentPage
{
    private readonly ApplicationDbContext ctx;

    public AddNewTypePage(ApplicationDbContext ctx)
	{
		InitializeComponent();
       
        this.ctx = ctx;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var existingCategories = await ctx.Categories
            .Select(c => new CategoryPickerModel
            {
                Id = c.Id,
                Name = c.Name,
            }).ToListAsync();

        CategoryList.ItemsSource = existingCategories;
    }

    private async void SaveButtonClicked(object sender, EventArgs e)
    {
        var typeName = AddTypeField.Text;

        var category = (CategoryPickerModel)CategoryList.SelectedItem;

        if (string.IsNullOrEmpty(typeName))
        {
            await DisplayAlert(
                MessagesConstants.WarningPopUpTitle,
                MessagesConstants.InvalidTypeNameMessage,
                MessagesConstants.PopUpClose);
        }
        else if(typeName.Length < 3 || typeName.Length > 30)
        {
            await DisplayAlert(
                MessagesConstants.WarningPopUpTitle,
                MessagesConstants.InvalidTypeNameLengthMessage,
                MessagesConstants.PopUpClose);
        }
        else if(category == null)
        {
            await DisplayAlert(
                MessagesConstants.WarningPopUpTitle, 
                MessagesConstants.InvalidCategoryMessage, 
                MessagesConstants.PopUpClose);
        }
        else
        {
            await SaveNewType(category.Id, typeName);
            await Shell.Current.GoToAsync($"{nameof(AddExpense)}");

        }

    }

    private async Task SaveNewType(int categoryId,string typeName)
    {
        
        var type = new ExpenseType()
        {
            Name = typeName,
            CategoryId = categoryId,
        };
        ctx.ExpenseTypes.Add(type);
        await ctx.SaveChangesAsync();
    }
}