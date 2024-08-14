using Employee_App.DTO;
using Employee_App.model;
using System.Text.Json;

namespace Employee_App.Services
{
    internal class ApiService : IApiService
    {
        public async Task<List<EmployeeAttendanceHours>> GetEmployeeTotalAttendanceHoursData(string apiUrl)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var attendances = JsonSerializer.Deserialize<List<Attendance>>(data)??new List<Attendance>();

                var employeeHours = attendances
                    .GroupBy(a => a.EmployeeName)
                    .Select(ag => new EmployeeAttendanceHours
                    {
                        EmployeeName = ag.Key,
                        TotalWorkHours = (int)ag.Sum(a => (a.EndTimeUtc - a.StarTimeUtc).TotalHours)
                    })
                    .OrderByDescending(eah => eah.TotalWorkHours)
                    .ToList();

                return employeeHours;
            }
            return new List<EmployeeAttendanceHours>();

        }
    }
}
