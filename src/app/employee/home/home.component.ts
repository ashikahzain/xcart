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
  constructor(public employeeservice: EmployeeService, public sidemenu: SidemenuComponent,private domSanitizer:DomSanitizer) { }

  toggle: boolean;

  ngOnInit(): void {
    //console.log("toggle"+this.toggle);
    this.employeeservice.getItems().subscribe(data => {
      this.itemList = data;
      let base64String = btoa(this.itemList[0].Image);
      console.log(base64String);
      this.imageurl = this.domSanitizer.bypassSecurityTrustUrl('data:image/jpg;base64, ' + base64String);
      console.log(this.imageurl);

    });
  }


}
