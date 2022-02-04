import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from 'src/app/shared/services/admin.service';
import { Overlay, OverlayRef } from "node_modules/@angular/cdk/overlay";
import {AddPointComponent} from '../add-point/add-point.component';
import { ComponentPortal } from "@angular/cdk/portal";

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  filter:string;
  overlayRef: OverlayRef;

  constructor(public adminService:AdminService,private router:Router,private overlay: Overlay) { }

  ngOnInit(): void {
    this.adminService.getAllEmployeesPoints();
  }

  awardHistory(UserId:number){
    this.router.navigate(['admin/awardHistory',UserId]);
  }


  open() {
    this.overlayRef = this.overlay.create();
    const componentPortal = new ComponentPortal(AddPointComponent);
    this.overlayRef.addPanelClass("example-overlay");
    this.overlayRef.attach(componentPortal);
  }
}
