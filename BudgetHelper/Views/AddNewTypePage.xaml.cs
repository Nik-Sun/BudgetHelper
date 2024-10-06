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

		//TODO:Add validity check
		var category = (CategoryPickerModel)CategoryList.SelectedItem;

        if (typeName.Length >= 3)
        {
            var type = new ExpenseType()
            {
                Name = typeName,
                CategoryId = category.Id,
            };
            ctx.ExpenseTypes.Add(type);
            ctx.SaveChanges();
        }

		await Shell.Current.GoToAsync($"{nameof(AddExpense)}");

    }
}