import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService } from 'src/app/shared/services/admin.service';
import {ValidateAwardName, ValidateNumbers} from 'src/app/shared/validators/formdatavalidator'

@Component({
  selector: 'app-add-award',
  templateUrl: './add-award.component.html',
  styleUrls: ['./add-award.component.css']
})
export class AddAwardComponent implements OnInit {
  
  awardForm: FormGroup;
  submitted = false;

  constructor(private formBuilder: FormBuilder, public adminService: AdminService) { }

  ngOnInit(): void {
    this.awardForm = this.formBuilder.group({
      Name: ['', [Validators.required, ValidateAwardName]],
      Points: ['', [Validators.required, ValidateNumbers]],
      IsActive: true
    });
  }
  // Get Controls
  get formControls(): any {
    return this.awardForm.controls;
  }
  addAward() {
    console.log(this.awardForm.value);
    this.adminService.addAward(this.awardForm.value).subscribe(
      (result) => console.log(result)
    );
  }

}
