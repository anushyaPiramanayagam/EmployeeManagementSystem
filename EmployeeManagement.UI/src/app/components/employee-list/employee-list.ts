import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmployeeService } from '../../services/employee';
import { Employee } from '../../models/employee';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './employee-list.html',
  styleUrl: './employee-list.css'
})
export class EmployeeListComponent implements OnInit {

  employees: Employee[] = [];

  constructor(private employeeService: EmployeeService) {}
ngOnInit(): void {

  this.employeeService.getEmployees().subscribe({
    next: (response) => {

      console.log('Response:', response);
      console.log('Success:', response.success);
      console.log('Data:', response.data);
      console.log('Length:', response.data.length);

      this.employees = response.data;

      console.log('Employees after assignment:', this.employees);

    },
    error: (err) => {
      console.error(err);
    }
  });

}
  
}