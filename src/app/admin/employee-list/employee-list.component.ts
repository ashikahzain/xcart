import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AdminService } from 'src/app/shared/services/admin.service';
import { ToastrService } from 'ngx-toastr';
import { PaginationService } from 'src/app/shared/services/pagination.service';
import { AllEmployeePoints } from 'src/app/shared/models/AllEmployeePoint';


@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  //declaring variables
  filter: string;
  addForm: FormGroup;
  error = '';
  display = 'none'; //default Variable
  presenteeId: number;
  today = new Date().toLocaleDateString();
  point: number;
  status: boolean;
  TotalEmployees: number;
  pagenumber: any = 1;
  pagesize: number = 10;
  paginationdata: number;
  exactPageList: number;
  pageField: any[];
  employeePointList: AllEmployeePoints[]
  pageNo: boolean[] = [];

  constructor(public adminService: AdminService, private router: Router, private formBuilder: FormBuilder,
    public toastr: ToastrService, public paginationService: PaginationService) { }

  ngOnInit(): void {
    this.paginationService.temppage = 0; 
    this.pageNo[0] = true;
    this.getEmployeeCount() ;
    this.paginationService.temppage = 0;  
    //get all employees points
    this.getAllEmployees()
    //get award list
    this.adminService.getAwards();

    //get the id of Admin
    this.presenteeId = Number(sessionStorage.getItem('userid'));

    //modal
    //creating form controls and validations
    this.addForm = this.formBuilder.group({
      Date: [],
      AwardId: ['', [Validators.required]],
      EmployeeId: [''],
      PresenteeId: [''],
      Point: [''],
      Status: ['']
    })


  }

  getAllEmployees() {
    //get the list of employees
    this.adminService.getAllEmployeesPoints(this.pagenumber, this.pagesize).subscribe(data => {
      this.employeePointList = data
      //this.TotalNumberofPages()
    }
    );
  }
  //get form controls
  get formControls() {
    return this.addForm.controls;
  }


  // Model Driven Form - login
  addPoint() {
    //console.log(this.addForm.value);

    //if data is invalid
    if (this.addForm.invalid) {
      this.error = "Invalid Input";
      return;
    }

    if (this.addForm.valid) {
      //add to award history table
      this.adminService.addAwardHistory(this.addForm.value).subscribe(data => {
        console.log(data);
      });

      //closing modal after adding point
      this.closeModalDialog();

      //Displaying message using toastr
      if (this.addForm.get('Status').value) {
        this.toastr.success(this.addForm.get('Point').value + " " + " Point Added Successfully!");
        window.setTimeout(function () { location.reload() }, 1000);
      }
      else {
        this.toastr.warning(this.addForm.get('Point').value + " " + "Point Removed Successfully!")
        window.setTimeout(function () { location.reload() }, 1000);


      }
      //console.log("added Point");
    }

  }

  //Function to open modal
  openAddModal(UserId: number, status: boolean) {
    this.display = 'block'; //Set block css

    //Assigning values to the form
    this.addForm.controls.EmployeeId.setValue(UserId);
    this.addForm.controls.PresenteeId.setValue(this.presenteeId);
    this.addForm.controls.Status.setValue(status);
    this.status = status;//setting status as true or false
    this.addForm.controls.Date.setValue(this.today);

  }

  //checking changes in select function
  onChange(val) {

    console.log("clicked");
    console.log(val);

    //getting point of the selected award
    let award = this.adminService.awardList.find(i => i.Id == val);
    this.point = Number(award.Points);
    //setting that point to the form
    this.addForm.controls.Point.setValue(this.point);

  }

  //function to close the modal
  closeModalDialog() {
    this.display = 'none'; //set none css after close dialog
    this.point = 0;
  }

  //Navigate to award history page along with userId of the selected employee
  awardHistory(UserId: number) {
    this.router.navigate(['admin/awardHistory', UserId]);
  }


  getEmployeeCount() {
    this.adminService.getEmployeeCount().subscribe(data => {
      this.TotalEmployees = data;
     this.TotalNumberofPages()
    });

  }
  TotalNumberofPages() {
    this.paginationdata = (this.TotalEmployees / this.pagesize);
    let tempPageData = this.paginationdata.toFixed();
    if (Number(tempPageData) < this.paginationdata) {
      this.exactPageList = Number(tempPageData) + 1;
      this.paginationService.exactPageList = this.exactPageList;
    }
    else {
      this.exactPageList = Number(tempPageData);
      this.paginationService.exactPageList = this.exactPageList
    }
    this.paginationService.pageOnLoad();
    this.pageField = this.paginationService.pageField;
  }


  showEmployeesByPageNumber(page, i) {
    this.employeePointList = [];
    this.pageNo = [];
    this.pageNo[i] = true;
    this.pagenumber = page;
    this.getAllEmployees();

  }
}
