import { Component, OnInit } from '@angular/core';
import 'chartjs-plugin-datalabels';
import { Chart, ChartDataset, ChartOptions, ChartType, registerables } from 'chart.js';
import ChartDataLabels from 'chartjs-plugin-datalabels';
import { EmployeeService } from 'src/app/_services/employee.service';
import {Attendance} from 'src/app/_models/Attendance';
import 'chartjs-plugin-datalabels';

Chart.register(...registerables, ChartDataLabels);
@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css'],
})
export class EmployeeListComponent implements OnInit {
  employeeHours: [string, number][] = [];
  chartData: ChartDataset[] = [];
  chartLabels: string[] = [];
  chartOptions: ChartOptions = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      datalabels: {
        display: true,
        color: '#000',
        formatter: (value: any, context: any) => {
          let dataArr: Number[] = context.chart.data.datasets[0].data;
          const total = dataArr.reduce(
            (previousValue, currentValue) =>
              Number(previousValue) + Number(currentValue)
          ) as number;
          let percentage = (value / total * 100).toFixed(0) + '%';
          return percentage;
        },
        font: {
          weight: 'bold'
        }
      },
      tooltip: {
        callbacks: {
          label: (context) => {
            const dataset = context.dataset;
            const value = Number(dataset.data[context.dataIndex]);
            const total = dataset.data.reduce(
              (previousValue, currentValue) =>
                Number(previousValue) + Number(currentValue)
            ) as number;
            const percentage = ((value / total) * 100).toFixed(2);
            return `Precentage : (${percentage}%) - (${value} hours)`;
          },
        },
      },
      legend: {
        position: 'bottom',
        align: 'center'
      },
    },
  };

  chartType: ChartType = 'pie';

  constructor(private employeeService: EmployeeService) {}

  ngOnInit() {
    this.employeeService.getAll().subscribe((data) => {
      this.employeeHours = this.calculteTotalAttendanceHoursForEmployee(data)
      this.getDataForChart();
    });
  }

  getDataForChart() {
    this.chartData = [
      {
        data: this.employeeHours.map((emplyeeHour) => emplyeeHour[1]),
        label: 'Total Work Hours',
      },
    ];
    this.chartLabels = this.employeeHours.map((emplyeeHour) => emplyeeHour[0]);
  }
  calculteTotalAttendanceHoursForEmployee(atendanceData: Attendance[]): [string, number][]{
    const employeeHours = new Map<string, number>();
    atendanceData.forEach(record => {
      const startTime = new Date(record.StarTimeUtc);
      const endTime = new Date(record.EndTimeUtc);

      const hoursWorked = (endTime.getTime() - startTime.getTime()) / (1000 * 60 * 60);

      if(employeeHours.has(record.EmployeeName)){
        employeeHours.set(record.EmployeeName, employeeHours.get(record.EmployeeName)! + hoursWorked);
      }
      else{
        employeeHours.set(record.EmployeeName, hoursWorked);
      }
    })
    employeeHours.forEach((hours, employeeName) => {
      employeeHours.set(employeeName, Math.round(hours));
  });
    const sortedEmployeeHoursArray = Array.from(employeeHours.entries()).sort((a, b) => b[1] - a[1]);
    return sortedEmployeeHoursArray;
  }
}
