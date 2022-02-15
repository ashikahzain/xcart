import { Component, OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/shared/services/employee.service';
import { CartService } from 'src/app/shared/services/cart.service';
import { CartViewModel } from 'src/app/shared/models/cartviewmodel';
import { Item } from 'src/app/shared/models/item';
import { DomSanitizer } from '@angular/platform-browser';
import { Order } from 'src/app/shared/models/order';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css'],
})
export class CartComponent implements OnInit {
  currentPoints: number;
  cart: CartViewModel;
  totalPoints: number = 0;
  order = new Order();
  itemQuantity = new Map<number, number>();
  checkQuantity: number = 1;
  ItemList: Item[] = [];
  cartcount: any;

  constructor(public cartservice: CartService, public employeeservice: EmployeeService,
    private domSanitizer: DomSanitizer, private toastr: ToastrService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.employeeservice.getItems().subscribe((data) => {
      this.ItemList = data;
    });

    this.cartservice.getAllCart().subscribe((data) => {
      console.log(data);
      this.cart = data;
      this.cartcount = data.length;
      data.forEach((item) => {
        item.ItemImage = this.domSanitizer.bypassSecurityTrustUrl(
          'data:image/jpg;base64,' + item.ItemImage
        ),
          item.ItemPoints = item.Quantity * item.ItemPoints;
        this.totalPoints += item.ItemPoints;
        this.itemQuantity.set(item.ItemId, item.Quantity);
      });
    });

    this.employeeservice.getCurrentPoints().subscribe((data) => {
      this.currentPoints = data;
    });
  }

  onDecrease(id: number) {
    this.cartservice.decreaseQuantity(id).subscribe();
    window.location.reload();
  }

  onIncrease(id: number) {
    this.cartservice.increaseQuantity(id).subscribe();
    window.location.reload();
  }

  deletefromCart(id: number) {
    if (confirm('Do you want to delete this item from cart?')) {
      this.cartservice.deletefromcart(id).subscribe();
    }
    window.location.reload();
  }

  async onCheckOut() {
    if (this.totalPoints <= this.currentPoints) {
      this.itemQuantity.forEach((item, key) => {
        var Item = this.ItemList.find((x) => x.Id == key);
        if (Item.Quantity < item) {
          this.checkQuantity = 0;
          this.toastr.error(Item.Name + ' only ' + Item.Quantity + ' left');
        }
      });

      this.order.DateOfDelivery = null,
        this.order.DateOfOrder = new Date().toLocaleDateString(),
        this.order.Points = this.totalPoints,
        this.order.StatusDescriptionId = 1,
        this.order.UserId = Number(sessionStorage.getItem('userid'));

      if (this.checkQuantity == 1) {
        if (confirm('Are you sure you want place the order?')) {
          this.cartservice.placeOrderFromCart(this.order).subscribe((data) => {
            console.log(data);
          });
          this.toastr.success(
            'Redeemed ' +
            this.totalPoints +
            ' Points. Contact HR Department to collect your items.'
          );
          this.router.navigateByUrl('employee/order');
        }
      }
    } else {
      this.toastr.error('Not Enough Points');
    }
  }
}
