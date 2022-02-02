import { Component, OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/shared/services/employee.service'
import { SidemenuComponent } from 'src/app/shared/layout/sidemenu/sidemenu.component'
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { Item } from 'src/app/shared/models/item';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  itemList: Item;
  imageurl: any;
  base64String: any;
  constructor(public employeeservice: EmployeeService, public sidemenu: SidemenuComponent, private domSanitizer: DomSanitizer) { }

  toggle: boolean;
  currentPoints: number;
  ngOnInit(): void {
    this.employeeservice.getItems().subscribe(data => {
      console.log(this.itemList);
      this.itemList=data
      data.forEach(item => {
        item.Image = this.domSanitizer.bypassSecurityTrustUrl('data:image/jpg;base64,' + item.Image)
        console.log(item.Image);
      });
      this.employeeservice.getCurrentPoints().subscribe(
        data => {
          //console.log("iside ts"+ data);
          this.currentPoints = data;
        }
      );
    });
  }

  testArray = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 171, 18, 19, 20];


}
