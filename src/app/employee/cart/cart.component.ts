import { Component, OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/shared/services/employee.service';
import { CartService } from 'src/app/shared/services/cart.service';
import { CartViewModel } from 'src/app/shared/models/cartviewmodel'

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  currentPoints: number;
  cart: CartViewModel;

  constructor(public cartservice: CartService, public employeeservice: EmployeeService) { }

  ngOnInit(): void {
    this.cartservice.getAllCart().subscribe(
      data => {
        console.log(data);
        this.cart = data;

      }
    );

    this.employeeservice.getCurrentPoints().subscribe(
      data => {
        this.currentPoints = data;
      });
  }


  onCheckOut(){
    console.log('Hi');
  }

  onDecrease(){
    //(this.cart.Quantity)++;
    console.log('Hi');
    console.log(this.cart);
    //console.log(typeof(this.cart.Quantity));
    //console.log(this.cart.Quantity);
  }
}
