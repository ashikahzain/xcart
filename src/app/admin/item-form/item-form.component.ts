import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AdminService } from 'src/app/shared/services/admin.service';
import { ValidateItemName, ValidateNumbers } from 'src/app/shared/validators/formdatavalidator'

@Component({
  selector: 'app-item-form',
  templateUrl: './item-form.component.html',
  styleUrls: ['./item-form.component.css']
})
export class ItemFormComponent implements OnInit {
  itemForm: FormGroup;
  selectedFile: File;
  imgsrc: any;
  Id: number;
  base64DefaultURL: string;
  imgUrl: string;
  editingmode: boolean = false;
  constructor(private formbuilder: FormBuilder, public adminService: AdminService, private route: ActivatedRoute, private domSanitizer: DomSanitizer, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {

    //for builder for item details
    this.itemForm = this.formbuilder.group({
      Id: 0,
      Name: ['', [Validators.required, ValidateItemName]],
      Image: '',
      Quantity: ['', [Validators.required, ValidateNumbers]],
      Points: ['', [Validators.required, ValidateNumbers]],
      IsActive: true
    }
    )

    //get Id from the route
    this.Id = this.route.snapshot.params['itemId'];
    if (this.Id != null) {
      this.editingmode = true;
      //populate form on init if id is not null value(for editing situations)
      this.populateItem();
    }
  }

  // Get Controls
  get formControls(): any {
    return this.itemForm.controls;
  }


  //function to change the file added to a base64 url
  onFileSelected(event) {
    const reader = new FileReader();
    if (event.target.files && event.target.files.length) { 
      const [file] = event.target.files;
      //read from file
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.imgsrc = reader.result as string;
        //b64 string conversion
        let b64Data = this.imgsrc.split(',', 2)[1];
        //assigning to image in item form
        this.itemForm.patchValue({
          Image: b64Data
        });
      };
    }
  }

  //on submit function
  onSubmit() {
    if (this.Id != null) {
      this.UpdateItem();
      this.toastr.success('Item edited successfully');
    }
    else {
      this.addItem();
      this.toastr.success('Item added successfully');
    }
    this.router.navigate(['/admin/catalogue']);
  }

  //add item
  addItem() {
    this.adminService.addItem(this.itemForm.value).subscribe();
  }

  //POPULATE ITEM TO FORM 
  populateItem() {
    //get item by id
    this.adminService.getItembyId(this.Id).subscribe(
      item => {
        //creating safe url for preview on form
        this.imgsrc = this.domSanitizer.bypassSecurityTrustUrl('data:image/jpg;base64,' + item.Image);
        //set value to the form group
        this.itemForm.setValue({
          Id: item.Id,
          Name: item.Name,
          Image: item.Image,
          Quantity: item.Quantity,
          Points: item.Points,
          IsActive: true
        });
      },
      error => console.log(error)
    );
  }


  //Updating Item details
  UpdateItem() {
    this.adminService.updateItem(this.itemForm.value).subscribe()
  }


}

