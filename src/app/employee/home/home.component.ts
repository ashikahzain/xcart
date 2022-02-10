import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { EmployeeService } from 'src/app/shared/services/employee.service'
import { SidemenuComponent } from 'src/app/shared/layout/sidemenu/sidemenu.component'
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { Item } from 'src/app/shared/models/item';
import { Cart } from 'src/app/shared/models/cart';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CartService } from 'src/app/shared/services/cart.service';
import { Order } from 'src/app/shared/models/order';
import { OrderDetails } from 'src/app/shared/models/OrderDetails';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  @ViewChild('child') private child: SidemenuComponent;
  filter: string;
  itemList: Item[];
  currentPoints: number;
  cart = new Cart();
  error = '';
  addForm: FormGroup;
  order = new Order();
  display = 'none';
  OrderId: number;
  itemid: number;
  points: number;
  orderdetails = new OrderDetails()
  availableQuantity: number;
  orderQuantity: any;
  constructor(public employeeservice: EmployeeService, public sidemenu: SidemenuComponent, private domSanitizer: DomSanitizer, private toastr: ToastrService, private formBuilder: FormBuilder, private Cartservice: CartService) { }

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

    //modal
    //creating form controls and validations
    this.addForm = this.formBuilder.group({
      Quantity: 1,
    })
  }


  // Model Driven Form - login
  addOrder() {

    //if data is invalid
    if (this.addForm.invalid) {
      this.error = "Invalid Input";
      return;
    }
    this.orderQuantity=this.addForm.controls.Quantity.value
    if (this.orderQuantity * this.points < this.currentPoints) {
      if (this.orderQuantity < this.availableQuantity) {
        if (this.addForm.valid) {
          //add to orderdetails table

          this.order.DateOfDelivery = null,
            this.order.DateOfOrder = new Date().toLocaleDateString(),
            this.order.Points = this.orderQuantity* this.points,
            this.order.StatusDescriptionId = 1,
            this.order.UserId = Number(sessionStorage.getItem('userid'))
          this.Cartservice.placeOrder(this.order).subscribe(
            data => {
              this.OrderId = data
              this.orderdetails.ItemId = this.itemid,
                this.orderdetails.Quantity = Number(this.addForm.controls.Quantity.value),
                this.orderdetails.OrderId = this.OrderId
              console.log(this.orderdetails)
              this.Cartservice.updateOrderDetails(this.orderdetails).subscribe(item => {
                console.log(item);
              });
            }
          );


          //closing modal after adding point
          this.closeModalDialog();
          this.toastr.success('ORDER ADDED');

          //console.log("added Point");
        }
      }
      else{
        this.toastr.error('Out of Stock')
      }
    }
    else {
      this.toastr.error("Not enough points")
    }


  }

  //Function to open modal
  openAddModal(itemId: number, points: number, quantity: number) {
    //Set block css
    this.display = 'block'
    console.log(this.addForm.value)
    this.itemid = itemId,
      this.points = points,
      this.availableQuantity = quantity
  }



  //function to close the modal
  closeModalDialog() {
    this.display = 'none'; //set none css after close dialog
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

  addtoCart(itemId: number) {
    console.log(itemId);
    this.cart.ItemId = itemId
    this.cart.Quantity = 1
    this.cart.UserId = Number(sessionStorage.getItem('userid'))
    this.employeeservice.addtoCart(this.cart).subscribe(
      data =>
        console.log(data)
    )
    this.toastr.success('added to cart');

  }



}



