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
  itemList: Item[];
  imageurl: any;
  constructor(public employeeservice: EmployeeService, public sidemenu: SidemenuComponent, private domSanitizer: DomSanitizer) { }

  toggle: boolean;
  currentPoints: number;
  ngOnInit(): void {

    //console.log("toggle"+this.toggle);

    this.employeeservice.getItems().subscribe(data => {

      this.itemList = data;

      //let base64String = btoa(this.itemList[0].Image);

      //this.imageurl = this.domSanitizer.bypassSecurityTrustUrl('data:image/jpg;base64, ' + base64String);

      //console.log(this.imageurl);

      this.employeeservice.getCurrentPoints().subscribe(

        data => {

          //console.log("iside ts"+ data);

          this.currentPoints = data;

        }

      );
    });

  }

  testArray = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 171, 18, 19, 20];

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



