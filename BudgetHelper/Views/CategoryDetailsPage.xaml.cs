using BudgetHelper.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls.Shapes;
using System.Globalization;

namespace BudgetHelper.Views;

public partial class CategoryDetailsPage : ContentPage , IQueryAttributable
{
    private readonly ApplicationDbContext ctx;
    private string date;
    private string categoryName;

    public CategoryDetailsPage(ApplicationDbContext ctx)
	{
		InitializeComponent();
        this.ctx = ctx;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        date = (string)query["Date"];
        categoryName = (string)query["Category"];
    }

    protected override async void OnAppearing()
    {
        var expenses = await GetExpensesForCategory();
        foreach (var expense in expenses)
        {
            var label = new Label
            {
                Text = expense,
                FontSize = 14,
                FontAttributes = FontAttributes.Bold,
                TextColor = Colors.Black,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(10)
            };
            var frame = new Border
            {
                Content = label,
                Stroke = Colors.DarkGray,
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius(10)
                },
                Padding = new Thickness(5),
                Margin = new Thickness(5),
              
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center

            };
            ParentStack.Children.Add(frame);
        }

       
       
        base.OnAppearing();
    }

    private async Task<List<string>> GetExpensesForCategory()
    {
        DateTime actualDate = DateTime.Parse(date);
        var expenses = await ctx.Expenses
            .Where(e => e.Created.Value.Month == actualDate.Month && e.Type.Category.Name == categoryName) 
            .Select(e => $"{e.Created.Value.ToString("d",CultureInfo.CurrentCulture)} : {e.Type.Name} - {e.Value :f2}лв")
            .ToListAsync();

        return expenses;
    }
}