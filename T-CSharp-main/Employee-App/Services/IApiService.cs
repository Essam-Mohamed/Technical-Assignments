using Employee_App.DTO;
using Employee_App.model;

namespace Employee_App.Services
{
    internal interface IApiService
    {
        Task<List<EmployeeAttendanceHours>> GetEmployeeTotalAttendanceHoursData(string apiUrl);
    }
}
