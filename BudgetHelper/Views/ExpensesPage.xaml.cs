using BudgetHelper.Infrastructure;
using BudgetHelper.Models;
using DevExpress.Maui.Charts;
using DevExpress.Maui.Core.Internal;
using Microsoft.Maui.Controls.Shapes;

namespace BudgetHelper.Views;

public partial class ExpensePage : ContentPage
{
    private bool isReload = false;
    private readonly ExpensePageModel _model;
    public ExpensePage(ExpensePageModel model)
    {
        InitializeComponent();
        _model = model;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (isReload == false)
        {
            LoadDataForMonth();
            var dates = _model.Dates;
            CustomDatePicker.ItemsSource = dates;
            UpdateSelectedIndexWithoutEvent();
        }
        else
        {
            var month = (string)CustomDatePicker.SelectedItem;
            LoadDataForMonth(month);
            isReload = false;
        }
        var totalValue = _model.DisplayData
           .Select(c => double.Parse(c.SliceValue))
           .Sum();

        TotalValueLabel.TextPattern = $"Общо\n{totalValue :f2}лв";
    }

    private void CustomDatePicker_SelectedIndexChanged(object? sender, EventArgs e)
    {

        var month = (string)CustomDatePicker.SelectedItem;
        LoadDataForMonth(month);

    }

    private void LoadDataForMonth(string? month = null)
    {
        _model.PopulateData(month);
        var data = _model.DisplayData;
        if (data.Count == 0 && month != null)
        {
            _model.PopulateData();
            data = _model.DisplayData;
            var dates = _model.Dates;
            CustomDatePicker.ItemsSource = dates;
            UpdateSelectedIndexWithoutEvent();
        }
        MyData.DataSource = data;

        MyView.Children.Clear();
        foreach (var entry in data)
        {
            var frame = CreateLabel(entry.SliceName, entry.SliceValue);
            MyView.Children.Add(frame);
        }
    }

    private Border CreateLabel(string name, string value)
    {


        var button = new Button()
        {
            Text = $"{name} => {value}лв",
            Background = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 1),
                GradientStops = new GradientStopCollection
                {
                    new GradientStop { Color = Colors.LightGray, Offset = 0.0f },
                    new GradientStop { Color = Colors.White, Offset = 1.0f }
                }
            },
            TextColor = Colors.Black,
            LineBreakMode = LineBreakMode.HeadTruncation

        };
        button.FontSize = Helpers.CalculateFontSize(button.Text.Length);
        button.Clicked += (sender, args) => OnCategoryClicked(sender!, args);

        var frame = new Border
        {
            Content = button,
            Stroke = Colors.DarkGray,
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(11)
            },
            Padding = new Thickness(5),
            Margin = new Thickness(5),

            Shadow = new Shadow
            {
                Brush = Brush.Black,  // Set shadow color
                Offset = new Point(5, 5), // Horizontal and vertical shadow offset
                Opacity = 0.6f, // Adjust the opacity of the shadow
                Radius = 10 // Blur radius 
            },
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center

        };


        return frame;
    }

    private async void OnCategoryClicked(object sender, EventArgs args)
    {
        isReload = true;
        var button = sender as Button;
        var categoryName = button.Text.Split(" => ")[0];
        var date = (string)CustomDatePicker.SelectedItem;
        var parameters = new Dictionary<string, object>()
        {
           { "Category",categoryName },
            {"Date",date }
        };

        await Shell.Current.GoToAsync($"{nameof(CategoryDetailsPage)}", parameters);
    }

    private void UpdateSelectedIndexWithoutEvent()
    {
        // Temporarily detach the event handler
        CustomDatePicker.SelectedIndexChanged -= CustomDatePicker_SelectedIndexChanged;

        // Update the SelectedIndex
        CustomDatePicker.SelectedIndex = CustomDatePicker.Items.Count - 1;

        // Reattach the event handler
        CustomDatePicker.SelectedIndexChanged += CustomDatePicker_SelectedIndexChanged;
    }




}