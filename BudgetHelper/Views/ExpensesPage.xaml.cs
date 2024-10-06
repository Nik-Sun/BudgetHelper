using BudgetHelper.Models;
using Microsoft.Maui.Controls.Shapes;

namespace BudgetHelper.Views;

public partial class ExpensePage : ContentPage
{
    private bool isReload = false;
    private readonly ChartViewModel _model;
    public ExpensePage(ChartViewModel model)
    {
        InitializeComponent();
        _model = model;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (isReload == false)
        {
            _model.PopulateData();

            var data = _model.DisplayData;
            var dates = _model.Dates;

            MyData.DataSource = data;
            CustomDatePicker.ItemsSource = dates;
            UpdateSelectedIndexWithoutEvent();
            CustomDatePicker.Title = dates[^1];
            MyView.Children.Clear();
            foreach (var entry in data)
            {
                var frame = CreateLabel(entry.SliceName, entry.SliceValue);


                MyView.Children.Add(frame);
            }
        }
        else
        {
            isReload = false;
        }

      
    }

    private void CustomDatePicker_SelectedIndexChanged(object? sender, EventArgs e)
    {

        var month = (string)CustomDatePicker.SelectedItem;
        _model.PopulateData(month);
        var data = _model.DisplayData;
        MyData.DataSource = data;
        CustomDatePicker.Title = month;
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
            FontSize = 15,
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
            HorizontalOptions = LayoutOptions.Center,
           // VerticalOptions = LayoutOptions.Center,

        };

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
        var date = CustomDatePicker.Title;
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