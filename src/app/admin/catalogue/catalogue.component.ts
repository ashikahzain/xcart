import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { Item } from 'src/app/shared/models/item';
import { AdminService } from 'src/app/shared/services/admin.service';
import { ConfirmationDialogService } from 'src/app/shared/services/confirmation-dialogue.service';
import { EmployeeService } from 'src/app/shared/services/employee.service';

@Component({
  selector: 'app-catalogue',
  templateUrl: './catalogue.component.html',
  styleUrls: ['./catalogue.component.css']
})
export class CatalogueComponent implements OnInit {
  itemList: Item[];
  filter: string;
  constructor(public employeeservice: EmployeeService, private domSanitizer: DomSanitizer, private router: Router, public adminService: AdminService,
              private confirmationDialogService:ConfirmationDialogService) { }

  ngOnInit(): void {
    //get all items for catalogue
    this.employeeservice.getItems().subscribe(data => {
      this.itemList = data
      data.forEach(item => {
        item.Image = this.domSanitizer.bypassSecurityTrustUrl('data:image/jpg;base64,' + item.Image)
      });
    });
  }

  //sort by point low to high
  sortPointAscending() {
    this.itemList.sort((a, b) =>
      a.Points - b.Points
    );
  }

  //sort by point high to low
  sortPointDescending() {
    this.itemList.sort((a, b) =>
      b.Points - a.Points
    );
  }

  //sort by availabilty low to high
  sortAvailibilityAscending() {
    this.itemList.sort((a, b) =>
      a.Quantity - b.Quantity
    );
  }

  //sort by availabilty high to low
  sortAvailibilityDescending() {
    this.itemList.sort((a, b) =>
      b.Quantity - a.Quantity
    );
  }

  //Edit item navigation to form with id
  edititem(itemId: number) {
    console.log(itemId);
    this.router.navigate(['admin/itemform', itemId]);

  }
  
  //delelte item using id
  deleteitem(itemid: number) {
    //if (confirm("This Item will be deleted from the catalogue"))
     

      this.confirmationDialogService.confirm('Delete item.', 'Are you sure you want to remove this item from catalogue?','Delete','Cancel')
      .then((confirmed) => {
        if(confirmed){
          console.log('User confirmed:', confirmed);
          this.adminService.deleteItem(itemid).subscribe(
            (result) => {
              window.location.reload()
            }
          );
        }
        else{
          console.log('User confirmed:', confirmed);
        }
      })
      .catch(() => console.log('User dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
  }

}
