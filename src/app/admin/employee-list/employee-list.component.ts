import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AdminService } from 'src/app/shared/services/admin.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  filter: string;
  addForm: FormGroup;
  error = '';
  display = 'none'; //default Variable
  presenteeId: number;
  today = new Date().toLocaleDateString();
  point: number;


  constructor(public adminService: AdminService, private router: Router, private formBuilder: FormBuilder,
    public toastr: ToastrService) { }

  ngOnInit(): void {
    this.adminService.getAllEmployeesPoints();
    this.adminService.getAwards();
    this.presenteeId = Number(sessionStorage.getItem('userid'));
    //modal
    //creating form controls and validations
    this.addForm = this.formBuilder.group({
      Date: [new Date()],
      AwardId: ['', [Validators.required]],
      EmployeeId: [''],
      PresenteeId: [''],
      Point: [''],
      Status: ['']
    })
  }

  awardHistory(UserId: number) {
    this.router.navigate(['admin/awardHistory', UserId]);
  }

  //get controls
  get formControls() {
    return this.addForm.controls;
  }


  // Model Driven Form - login
  addPoint() {
    console.log(this.addForm.value);

    //if data is invalid
    if (this.addForm.invalid) {
      this.error = "Invalid Input";
      return;
    }

    if (this.addForm.valid) {
      this.adminService.addAwardHistory(this.addForm.value).subscribe(data => {
        console.log(data);
      });

      this.closeModalDialog();

      if (this.addForm.get('Status').value) {
        this.toastr.success(this.point + " " + " Point Added Successfully!");
        window.setTimeout(function(){location.reload()},1000);
      }
      else {
        this.toastr.warning(this.point + " " + "Point Removed Successfully!")
        window.setTimeout(function(){location.reload()},1000);


      }
      console.log("added Point");
    }

  }

  openAddModal(UserId: number, status: number) {
    this.display = 'block'; //Set block css
    this.addForm.controls.EmployeeId.setValue(UserId);
    this.addForm.controls.PresenteeId.setValue(this.presenteeId);
    this.addForm.controls.Status.setValue(status);
    this.addForm.controls.Date.setValue(this.today);

  }

  closeModalDialog() {
    this.display = 'none'; //set none css after close dialog
  }

  onChange(val) {

    console.log("clicked");
    console.log(val);

    let award = this.adminService.awardList.find(i => i.Id == val);
    this.point = Number(award.Points);
    this.addForm.controls.Point.setValue(this.point);

  }
}
