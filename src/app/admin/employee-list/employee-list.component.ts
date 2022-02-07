import { Component, OnInit } from '@angular/core';
import { FormGroup,FormBuilder,FormControl,Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AdminService } from 'src/app/shared/services/admin.service';
import {AddPointComponent} from '../add-point/add-point.component';


@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  filter:string;
  addForm:FormGroup;
  error='';
  display='none'; //default Variable


  constructor(public adminService:AdminService,private router:Router,private formBuilder:FormBuilder) { }

  ngOnInit(): void {
    this.adminService.getAllEmployeesPoints();

    //modal
    //creating form controls and validations
    this.addForm=this.formBuilder.group({
      Date:[new Date()],
      AwardId:['',[Validators.required]],
      EmployeeId:[''],
      PresenteeId:[''],
      Point:[''],
      Status:['']
    })
  }

  awardHistory(UserId:number){
    this.router.navigate(['admin/awardHistory',UserId]);
  }

    //get controls
    get formControls(){
      return this.addForm.controls;
    }


  // Model Driven Form - login
    addPoint(){
      console.log(this.addForm.value);

          //if data is invalid
    if(this.addForm.invalid){
      this.error="Invalid Input";
      return;
    }

    if(this.addForm.valid){
      console.log("added Point"+this.addForm.value);
    }

    }

  openModalDialog(){
    this.display='block'; //Set block css
 }

 closeModalDialog(){
  this.display='none'; //set none css after close dialog
 }
}
