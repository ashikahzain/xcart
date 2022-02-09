import { Component, OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/shared/services/employee.service';
import { CartService } from 'src/app/shared/services/cart.service';
import { CartViewModel } from 'src/app/shared/models/cartviewmodel'
import { Item } from 'src/app/shared/models/item';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  currentPoints: number;
  cart: CartViewModel;
  totalPoints:number=0;

  constructor(public cartservice: CartService, public employeeservice: EmployeeService, private domSanitizer: DomSanitizer) { }

  ngOnInit(): void {
    this.cartservice.getAllCart().subscribe(
      data => {
        console.log(data);
        this.cart = data;
        data.forEach(item =>{
          item.ItemImage = this.domSanitizer.bypassSecurityTrustUrl('data:image/jpg;base64,' + item.ItemImage),
          this.totalPoints +=item.Quantity*item.ItemPoints;
        } );
      }
    );

    this.employeeservice.getCurrentPoints().subscribe(
      data => {
        this.currentPoints = data;
      });
  }


  onCheckOut() {
    console.log('Hi');
  }

  onDecrease(id: number) {
    this.cartservice.decreaseQuantity(id).subscribe();
    window.location.reload();
  }

  onIncrease(id: number) {
    this.cartservice.increaseQuantity(id).subscribe();
    window.location.reload();
  }
}
