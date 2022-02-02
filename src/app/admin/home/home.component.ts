import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/shared/services/admin.service';
import { AuthService } from 'src/app/shared/services/auth.service';
import { EmployeeService } from 'src/app/shared/services/employee.service';
import { MostAwarded } from 'src/app/shared/models/MostAwarded'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  filter: string;
  employee: MostAwarded;
  constructor(public orderService: AdminService, private authservice: AuthService, 
    public employeeservice:EmployeeService) { }

  ngOnInit(): void {
    this.orderService.getOrder();
    this.employeeservice.getMostAwardedEmployee().subscribe(data=>{
this.employee=data;
    });

  }

}
