using Aspose.Html;
using Employee_App.DTO;

namespace Employee_App.Utils
{
    internal class Html : IHtml
    {
        public void GenerateHtmlTable(List<EmployeeAttendanceHours> employeeWorkHours)
        {
            // Create an HTML document
            using (var document = new HTMLDocument())
            {
                // Create the table element
                var table = (HTMLTableElement)document.CreateElement("table");
                table.Style.TextAlign = "center";
                table.Style.Margin = "0 auto";
                table.Style.BorderCollapse = "collapse";
                table.Style.Border = "1px solid lightgray";

                // Add table headers
                var headers = new[] { "Name", "Total Time in Month" };
                var headerRow = (HTMLTableRowElement)document.CreateElement("tr");
                foreach (var header in headers)
                {
                    var th = (HTMLTableCellElement)document.CreateElement("th");
                    th.TextContent = header;
                    th.Style.BackgroundColor = "gray";
                    th.Style.Border = "1px solid lightgray";
                    th.Style.Padding = "8px";
                    headerRow.AppendChild(th);
                }
                table.AppendChild(headerRow);

                // Add table rows
                foreach (var entity in employeeWorkHours)
                {
                    var row = (HTMLTableRowElement)document.CreateElement("tr");

                    var nameCell = (HTMLTableCellElement)document.CreateElement("td");
                    nameCell.TextContent = entity.EmployeeName;
                    nameCell.Style.Border = "1px solid lightgray";
                    nameCell.Style.Padding = "8px";
                    row.AppendChild(nameCell);

                    var timeWorkedCell = (HTMLTableCellElement)document.CreateElement("td");
                    timeWorkedCell.TextContent = entity.TotalWorkHours.ToString();
                    timeWorkedCell.Style.Border = "1px solid lightgray";
                    timeWorkedCell.Style.Padding = "8px";
                    row.AppendChild(timeWorkedCell);



                    // Add color to the row if time worked is less than 100 hours
                    if (entity.TotalWorkHours < 100)
                    {
                        row.Style.BackgroundColor = "rgb(252, 111, 111)";
                    }

                    table.AppendChild(row);
                }

                document.Body.AppendChild(table);

                document.Save("../../../generated html page/employeesTimeWorked.html");

            }
        }
    }
}
