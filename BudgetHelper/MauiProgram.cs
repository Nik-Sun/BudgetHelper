using BudgetHelper.Core;
using BudgetHelper.Views;
using Microsoft.Extensions.Logging;

namespace BudgetHelper
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .Services.AddSingleton<ApplicationDbContext>()
                .AddTransient<MainPage>()
                .AddTransient<AddExpense>();
            // builder.Services.AddSingleton<TodoItemDatabase>();
            //builder.Services.AddTransient<MainPage>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
