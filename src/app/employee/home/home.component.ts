import { Component, OnInit, ViewChild } from '@angular/core';
import { EmployeeService } from 'src/app/shared/services/employee.service'
import { SidemenuComponent } from 'src/app/shared/layout/sidemenu/sidemenu.component'
import { DomSanitizer } from '@angular/platform-browser';
import { Item } from 'src/app/shared/models/item';
import { Cart } from 'src/app/shared/models/cart';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormGroup } from '@angular/forms';
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
  cartList: number[] = [];
  constructor(public employeeservice: EmployeeService, public sidemenu: SidemenuComponent, private domSanitizer: DomSanitizer, private toastr: ToastrService, private formBuilder: FormBuilder, private Cartservice: CartService) { }

  ngOnInit(): void {

    //get all items 
    this.employeeservice.getItems().subscribe(data => {
      this.itemList = data
      data.forEach(item => {
        item.Image = this.domSanitizer.bypassSecurityTrustUrl('data:image/jpg;base64,' + item.Image)
      });
      this.employeeservice.getCurrentPoints().subscribe(
        data => {
          this.currentPoints = data;
        }
      );
    });

    //modal
    //creating form controls and validations
    this.addForm = this.formBuilder.group({
      Quantity: 1,
    })

    //get items already added to the cart
    this.Cartservice.getAllCart().subscribe(
      data => {
        data.forEach(item => {
          this.cartList.push(item.ItemId)
        }
        );
      }
    )

  }

  //compare cart and items
  compareCart(itemId: number): number {
    if (this.cartList.indexOf(itemId) < 0) {
      return 1;
    }
    else {
      return 0;
    }
  }

  // Model Driven Form - to buy item directly
  addOrder() {

    //if data is invalid
    if (this.addForm.invalid) {
      this.error = "Invalid Input";
      return;
    }

    //assigning quantity entered
    this.orderQuantity = this.addForm.controls.Quantity.value
    //comparing total order points to points remaining
    if (this.orderQuantity * this.points < this.currentPoints) {
      //comparing total order quantity to remaining quantity
      if (this.orderQuantity < this.availableQuantity) {
        if (this.addForm.valid) {

          //add to orderdetails table
          this.order.DateOfDelivery = null,
            this.order.DateOfOrder = new Date().toLocaleDateString(),
            this.order.Points = this.orderQuantity * this.points,
            this.order.StatusDescriptionId = 1,
            this.order.UserId = Number(sessionStorage.getItem('userid'))
          this.Cartservice.placeOrder(this.order).subscribe(
            data => {
              this.OrderId = data
              this.orderdetails.ItemId = this.itemid,
                this.orderdetails.Quantity = Number(this.addForm.controls.Quantity.value),
                this.orderdetails.OrderId = this.OrderId
              this.Cartservice.updateOrderDetails(this.orderdetails).subscribe();
            }
          );
          //closing modal after placing order
          this.closeModalDialog();
          this.toastr.success('Redeemed ' + this.order.Points + ' Points. Contact HR Department to collect your items.')
        }
      }
      else {
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
  }

  sortPointDescending() {
    this.itemList.sort((a, b) =>
      b.Points - a.Points
    );
  }

  sortAvailibilityAscending() {
    this.itemList.sort((a, b) =>
      a.Quantity - b.Quantity
    );
  }

  sortAvailibilityDescending() {
    this.itemList.sort((a, b) =>
      b.Quantity - a.Quantity
    );
  }

  //adding a product to cart
  addtoCart(itemId: number) {
    this.cart.ItemId = itemId
    this.cart.Quantity = 1
    this.cart.UserId = Number(sessionStorage.getItem('userid'))
    this.employeeservice.addtoCart(this.cart).subscribe()
    this.toastr.success('Added to cart');
    window.setTimeout(function () { location.reload() }, 1000);

  }

}



