import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { EmployeeService } from 'src/app/shared/services/employee.service'
import { SidemenuComponent } from 'src/app/shared/layout/sidemenu/sidemenu.component'
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { Item } from 'src/app/shared/models/item';
import { Cart } from 'src/app/shared/models/cart';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  @ViewChild('child') private child: SidemenuComponent;
  filter: string ;
  itemList: Item[];
  currentPoints: number;
  cart= new Cart();

  constructor(public employeeservice: EmployeeService, public sidemenu: SidemenuComponent, private domSanitizer: DomSanitizer,private toastr:ToastrService) { }
  
  ngOnInit(): void {
    this.employeeservice.getItems().subscribe(data => {
      console.log(this.itemList);
      this.itemList = data
      data.forEach(item => {
        item.Image = this.domSanitizer.bypassSecurityTrustUrl('data:image/jpg;base64,' + item.Image)
        console.log(item.Image);
      });
      this.employeeservice.getCurrentPoints().subscribe(

        data => {

          //console.log("iside ts"+ data);

          this.currentPoints = data;

        }

      );
    });
  }


  //Sorting
  sortPointAscending() {

    this.itemList.sort((a, b) =>
      a.Points - b.Points
    );
    console.log(this.itemList);
  }


  sortPointDescending() {

    this.itemList.sort((a, b) =>
      b.Points - a.Points
    );
    console.log(this.itemList);
  }


  sortAvailibilityAscending() {

    this.itemList.sort((a, b) =>
      a.Quantity - b.Quantity
    );
    console.log(this.itemList);
  }


  sortAvailibilityDescending() {

    this.itemList.sort((a, b) =>
      b.Quantity - a.Quantity
    );
    console.log(this.itemList);
  }

  addtoCart(itemId:number){
    console.log(itemId);
    this.cart.ItemId=itemId
    this.cart.Quantity=1
    this.cart.UserId=Number(sessionStorage.getItem('userid'))
  this.employeeservice.addtoCart(this.cart).subscribe(
    data=>
    console.log(data)
  )
  this.toastr.success('added to cart');
  
  }

}



