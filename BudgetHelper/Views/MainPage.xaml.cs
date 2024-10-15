using BudgetHelper.Core;
using BudgetHelper.Models;
using CommunityToolkit.Maui.Behaviors;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BudgetHelper.Views
{
    public partial class MainPage : ContentPage
    {

        private readonly QuoteProvider provider;

        public MainPage(QuoteProvider provider)
        {
            InitializeComponent();
            this.provider = provider;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            QuoteLabel.Text = "Loading...";
            await provider.UpdateQuote();
            QuoteLabel.Text = provider.CurrentQuote;
            if (!QuoteLabel.Behaviors.OfType<TouchBehavior>().Any())
            {
                QuoteLabel.Behaviors.Add(TouchBehaviour());
            }

        }

        private TouchBehavior TouchBehaviour()
        {
            return new TouchBehavior()
            {
                DefaultAnimationDuration = 250,
                DefaultAnimationEasing = Easing.CubicInOut,
                PressedOpacity = 0.6,
                PressedScale = 0.8,
                Command = new Command(async () =>
                {
                    QuoteLabel.Text = "Loading...";
                    provider.isLoaded = false;
                    await provider.UpdateQuote();
                    QuoteLabel.Text = provider.CurrentQuote;
                })
            };

        }
    }

}
