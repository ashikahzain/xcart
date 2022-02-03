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
  filter : string;
  itemList: Item[];
  imageurl: any;
  base64String: any;
  constructor(public employeeservice: EmployeeService, public sidemenu: SidemenuComponent, private domSanitizer: DomSanitizer) { }

  toggle: boolean;
  currentPoints: number;
  ngOnInit(): void {
    this.filter = this.sidemenu.filter;
    this.employeeservice.getItems().subscribe(data => {
      console.log(this.itemList);
      this.itemList = data
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


  //Sorting
  sortPointAscending() {

    this.itemList.sort((a, b) =>
      parseFloat(a.Points) - parseFloat(b.Points)
    );
    console.log(this.itemList);
  }


  sortPointDescending() {

    this.itemList.sort((a, b) =>
      parseFloat(b.Points) - parseFloat(a.Points)
    );
    console.log(this.itemList);
  }


  sortAvailibilityAscending() {

    this.itemList.sort((a, b) =>
      parseFloat(a.Quantity) - parseFloat(b.Quantity)
    );
    console.log(this.itemList);
  }


  sortAvailibilityDescending() {

    this.itemList.sort((a, b) =>
      parseFloat(b.Quantity) - parseFloat(a.Quantity)
    );
    console.log(this.itemList);
  }

}



