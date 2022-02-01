import { Component, OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/shared/services/employee.service'
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
 
  constructor(public employeeservice: EmployeeService) { }

  ngOnInit(): void {
    this.employeeservice.getItems();
    console.log(this.employeeservice.itemList);
  }
testArray=[1,2,3,4,5,6,7,8];

}
