using Employee_App.DTO;
using Employee_App.model;
using Employee_App.Services;
using Employee_App.Utils;
using Microsoft.Extensions.DependencyInjection;
using OxyPlot.Series;
using OxyPlot;
using OxyPlot.SkiaSharp;
using OxyPlot.Legends;

namespace Employee_App
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var Services = new ServiceCollection();


            // Register the services and dependencies
            Services.AddScoped<IApiService, ApiService>();
            Services.AddScoped<IHtml, Html>();
            Services.AddScoped<IChartService, Chart>();


            // Build the service provider
            var ServiceProvider = Services.BuildServiceProvider();


            //Resolve from the service provider
            var ApiService = ServiceProvider.GetService<IApiService>();
            var HtmlService = ServiceProvider.GetService<IHtml>();
            var ChartService = ServiceProvider.GetService<IChartService>();



            string apiUrl = "https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";

            // get list of employees name with thier worked hours
            var employeeHours = await ApiService.GetEmployeeTotalAttendanceHoursData(apiUrl);

            // generate image of pie chart for emloyees and total time worked
            ChartService.GeneratePieChart(employeeHours);

            //generate html page contain table of employee's name and total time worked
            HtmlService.GenerateHtmlTable(employeeHours);

        }
    }
}