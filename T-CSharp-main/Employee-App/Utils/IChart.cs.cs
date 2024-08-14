using Employee_App.DTO;

namespace Employee_App.Utils
{
    internal interface IChartService
    {
        void GeneratePieChart(List<EmployeeAttendanceHours> employeeHours);
    }
}
