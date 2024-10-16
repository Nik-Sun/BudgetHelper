﻿using BudgetHelper.Core;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BudgetHelper.Models
{
    public class ExpensePageModel
    {
        private readonly Color[] palette;
        public Color[] Palette => palette;
        private readonly ApplicationDbContext ctx;
        private IReadOnlyList<ChartSlice> displayData;
        private List<string> dates;


        public ExpensePageModel(ApplicationDbContext ctx)
        {
            this.ctx = ctx;

            palette = PaletteLoader.LoadPalette("#975ba5",
                "#03bfc1",
                "#f8c855",
                "#f45a4e",
                "#496cbe",
                "#f58f35", 
                "#d293fd", 
                "#25a966");
        }


        public async void PopulateData(string? date = null)
        {
            if(date != null)
            {
                //Picker changed
                var result = await GetDataForChart(date);
                displayData = result;
            }
            else 
            {
                //Page reload
                var result = await GetDataForChart(DateTime.Now.ToString("Y"));
                PopulateDates();
                if (result.Count == 0 && this.dates.Count != 0) 
                {
                    //No data for current month
                    result = await GetDataForChart(this.Dates[^1]);
                }
               
                displayData = result;
            }
            
        }

        public async void PopulateDates()
        {
            var dates = await ctx.Expenses
            .OrderBy(e => e.Created)
            .Select(e => e.Created.ToString("Y",new CultureInfo("BG-bg")))
            .ToListAsync();

            if (dates.Count == 0)
            {
                dates.Add(DateTime.Now.ToString("Y"));
            }
            this.dates = dates.Distinct().ToList();
        }

        private async Task<List<ChartSlice>> GetDataForChart(string date)
        {
            var query = await ctx.Expenses
                .Include(e => e.Type)
                .ThenInclude(e => e.Category)
                .ToListAsync();
            var actualDate = DateTime.Parse(date, new CultureInfo("BG-bg"));

            var result = query
                .Where(e => e.Created.Month == actualDate.Month && e.Created.Year == actualDate.Year)
                .GroupBy(x => x.Type.Category.Name)
             .Select(x => new ChartSlice
             {
                 SliceName = x.Key,
                 SliceValue = x.Sum(x => x.Value).ToString("F2")
             }).ToList();

            return result;
        }

        public IReadOnlyList<ChartSlice> DisplayData => displayData;
        public List<string> Dates => dates;


    }

    public class ChartSlice
    {

        public ChartSlice()
        {

        }

        public string SliceValue { get; set; }
        public string SliceName { get; set; }

    }


    static class PaletteLoader
    {
        public static Color[] LoadPalette(params string[] values)
        {
            Color[] colors = new Color[values.Length];
            for (int i = 0; i < values.Length; i++)
                colors[i] = Color.FromArgb(values[i]);
            return colors;
        }
    }

}
