import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/shared/services/admin.service';
import { AuthService } from 'src/app/shared/services/auth.service';
import { EmployeeService } from 'src/app/shared/services/employee.service';
import { MostAwarded } from 'src/app/shared/models/MostAwarded'
import { Item } from 'src/app/shared/models/item';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  filter: string;
  employee: MostAwarded;
  trendingItemList:Item;

  constructor(public adminService: AdminService, private authservice: AuthService,
    public employeeservice: EmployeeService,private domSanitizer:DomSanitizer,private router:Router) { }

  ngOnInit(): void {
    this.adminService.getOrder();

    this.employeeservice.getMostAwardedEmployee().subscribe(data => {
      this.employee = data;
    });

    this.adminService.getTrendingItems().subscribe(data=>{
      this.trendingItemList=data
      data.forEach(item => {
        item.Image = this.domSanitizer.bypassSecurityTrustUrl('data:image/jpg;base64,' + item.Image)
        console.log(item.Image);
      });
    }
      );
  }

  OrderDetails(){
    this.router.navigateByUrl('admin/orderdetails');
  }
  toEmployeeList(){
    this.router.navigateByUrl('/admin/employeeList')
  }

}
