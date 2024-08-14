using Employee_App.DTO;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;
using OxyPlot;
using System.Drawing;

namespace Employee_App.Utils
{
    internal class Chart : IChartService
    {
        public void GeneratePieChart(List<EmployeeAttendanceHours> employeeHours)
        {
            var plotModel = new PlotModel
            {
                Title = "Employee Work Hours",
                Background = OxyColors.White
            };
            var pieSeries = new PieSeries
            {
                StrokeThickness = 1.0,
                Diameter = 0.8,
                InsideLabelPosition = 0.75,
                AngleSpan = 360,
                StartAngle = 0,
                InsideLabelFormat = "{1}: {2:0}%",
                FontSize = 10
            };

            double totalHours = employeeHours.Sum(e => e.TotalWorkHours);

            foreach (var entity in employeeHours)
            {
                pieSeries.Slices.Add(new PieSlice(entity.EmployeeName, entity.TotalWorkHours / totalHours * 100)
                {
                    IsExploded = false,
                });
            }

            plotModel.Series.Add(pieSeries);

            // Export the plot to a PNG file
            var pngExporter = new PngExporter { Width = 600, Height = 600 };
            using (var stream = File.OpenWrite("../../../generated image/EmployeePieChart.png"))
            {
                pngExporter.Export(plotModel, stream);
            }
        }
    }
}

