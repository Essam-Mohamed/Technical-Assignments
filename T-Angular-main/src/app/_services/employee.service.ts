import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Attendance } from '../_models/Attendance';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  baseUrl =
    'https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ== ';

  getAll() {
    return this.http.get<Attendance[]>(this.baseUrl);
  }

  constructor(private http: HttpClient) {}
}
