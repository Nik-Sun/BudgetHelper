using BudgetHelper.Core;
using BudgetHelper.Models;
using BudgetHelper.Views;
using DevExpress.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
#if ANDROID
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using System;
#endif


namespace BudgetHelper
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseDevExpress()
                .UseDevExpressCharts()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("roboto-bold.ttf", "Roboto-Bold");
                    fonts.AddFont("roboto-regular.ttf", "Roboto");
                })
                .Services.AddDbContext<ApplicationDbContext>(opt =>
                {
                    opt.UseSqlite("Filename=app.db");
                })
                .AddTransient<MainPage>()
                .AddTransient<AddExpense>()
                .AddTransient<ChartViewModel>()
                .AddTransient<CategoryDetailsPage>()
                .AddTransient<ExpensePage>()
                .AddTransient<AddNewTypePage>();
            

            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("Placeholder", (h, v) =>
            {
#if ANDROID
                h.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
#endif
#if IOS
                h.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
            });
            Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping("PickerCustomization", (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
#endif
            });
#if DEBUG
            builder.Logging.AddDebug();
#endif

             var app =builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate(); // Apply pending migrations
            }
            return app;
        }
    }
}
