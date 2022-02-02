import { Component, OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/shared/services/employee.service'
import { SidemenuComponent } from 'src/app/shared/layout/sidemenu/sidemenu.component'
import { DomSanitizer } from '@angular/platform-browser';
import { Item } from 'src/app/shared/models/item';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  itemList: Item;
  constructor(public employeeservice: EmployeeService, public sidemenu: SidemenuComponent) { }

  toggle: boolean;

  ngOnInit(): void {
    //console.log("toggle"+this.toggle);
    this.employeeservice.getItems().subscribe(data => {
      this.itemList = data;
      this.itemList.Image=btoa(data);
      console.log(this.itemList);;
      console.log(this.employeeservice.itemList);

    });
  }


}
