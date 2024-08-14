using Employee_App.DTO;

namespace Employee_App.Utils
{
    internal interface IHtml
    {
        void GenerateHtmlTable(List<EmployeeAttendanceHours> employeeWorkHours);
    }
}
