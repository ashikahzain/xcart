import { Component, OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/shared/services/employee.service'
import {SidemenuComponent} from 'src/app/shared/layout/sidemenu/sidemenu.component'
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
 
  constructor(public employeeservice: EmployeeService,public sidemenu:SidemenuComponent) { }

   toggle:boolean;  
   currentPoints: number;

  ngOnInit(): void {
    this.employeeservice.getItems();
    //console.log(this.employeeservice.itemList);

    this.employeeservice.getCurrentPoints().subscribe(
      data=>{
        //console.log("iside ts"+ data);
        this.currentPoints=data;
      }
    );
    

  }

  testArray=[1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,171,18,19,20];

  
}
