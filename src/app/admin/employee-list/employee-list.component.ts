import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from 'src/app/shared/services/admin.service';
import { Overlay, OverlayRef } from "@angular/cdk/overlay";

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  filter:string;
  constructor(public adminService:AdminService,private router:Router) { }

  ngOnInit(): void {
    this.adminService.getAllEmployeesPoints();
  }

  awardHistory(UserId:number){
    this.router.navigate(['admin/awardHistory',UserId]);
  }

}
