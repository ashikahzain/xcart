import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService } from 'src/app/shared/services/admin.service';
import {ValidateItemName, ValidateNumbers} from 'src/app/shared/validators/formdatavalidator'

@Component({
  selector: 'app-item-form',
  templateUrl: './item-form.component.html',
  styleUrls: ['./item-form.component.css']
})
export class ItemFormComponent implements OnInit {
  itemForm: FormGroup;
  selectedFile: File;
  imgsrc: String;
  constructor(private formbuilder: FormBuilder, public adminService: AdminService) { }

  ngOnInit(): void {
    this.itemForm = this.formbuilder.group({
      Name: ['', [Validators.required, ValidateItemName]],
      Image: '',
      Quantity:  ['', [Validators.required, ValidateNumbers]],
      Points:  ['', [Validators.required, ValidateNumbers]],
      IsActive: true
    }
    )
  }

  // Get Controls
  get formControls(): any {
    return this.itemForm.controls;
  }



  onFileSelected(event) {
    const reader = new FileReader();
    if (event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.imgsrc = reader.result as string;
        let b64Data = this.imgsrc.split(',', 2)[1];
        this.itemForm.patchValue({
          Image: b64Data

        });
      };
    }
  }
  addItem() {
    console.log(this.itemForm.value);
    this.adminService.addItem(this.itemForm.value).subscribe(
      (result) => console.log(result)
    );
  }

}

