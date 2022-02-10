import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService } from 'src/app/shared/services/admin.service';
import {ValidateAwardName, ValidateNumbers} from 'src/app/shared/validators/formdatavalidator'
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-add-award',
  templateUrl: './add-award.component.html',
  styleUrls: ['./add-award.component.css']
})
export class AddAwardComponent implements OnInit {
  
  awardForm: FormGroup;
  submitted = false;
  Id: number;
  formTitle = 'Add new Award';
  buttonTitle='Add Award';

  constructor(private formBuilder: FormBuilder, public adminService: AdminService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.awardForm = this.formBuilder.group({
      Id: 0,
      Name: ['', [Validators.required, ValidateAwardName]],
      Points: ['', [Validators.required, ValidateNumbers]],
      IsActive: true
    });
    //get Id from the route
    this.Id = this.route.snapshot.params['awardId'];
    console.log(this.Id)
    if (this.Id != null) {
      this.formTitle = 'Update Award';
      this.buttonTitle = 'Update';
      //populate form on init
      this.populateAward();
    }
  }
  // Get Controls
  get formControls(): any {
    return this.awardForm.controls;
  }
  //on submit function
  onSubmit() {
    if (this.Id != null) {
      this.updateAward();
    }
    else {
      this.addAward();
    }
    this.router.navigate(['admin/award']).then(() => {
      window.location.reload();
    });
  }
  addAward() {
    console.log(this.awardForm.value);
    this.adminService.addAward(this.awardForm.value).subscribe(
      (result) => console.log(result)
    );
  }

  //POPULATE AWARD TO FORM 
  populateAward() {
    //get award by id
    this.adminService.getAwardbyId(this.Id).subscribe(
      award => {
        this.awardForm.setValue({
          Id: award.Id,
          Name: award.Name,
          Points: award.Points,
          IsActive: true
        });
      },
      error => console.log(error)
    );
  }


  //UPDATING AWARD
  updateAward() {
    console.log(this.awardForm.value);
    this.adminService.updateAward(this.awardForm.value).subscribe(
      (result) => {
        console.log(result);
      }
    )
  }
}
