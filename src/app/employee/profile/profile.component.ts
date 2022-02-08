import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { SidemenuComponent } from 'src/app/shared/layout/sidemenu/sidemenu.component';
import { Employeeprofile } from 'src/app/shared/models/employeeprofile'
import { EmployeeService } from 'src/app/shared/services/employee.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  EmployeeById: Employeeprofile[];
  image: string;

  constructor(public employeeservice: EmployeeService, public sidemenu: SidemenuComponent, private domSanitizer: DomSanitizer) { }

  ngOnInit(): void {
    this.employeeservice.getEmployeeProfile().subscribe(
      data => {
        console.log("Before assigning");
        console.log(this.EmployeeById);
        console.log(data);
        /*
        data.forEach(item =>{
          item.Image = this.domSanitizer.bypassSecurityTrustUrl('data:image/jpg;base64,' +item.Image);
        })
        */
        //data.Image = this.domSanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' +data.Image);
        this.image=data.Image;
        this.EmployeeById = data;
        console.log("After assigning");
        console.log(this.EmployeeById);
      });
  }

}
