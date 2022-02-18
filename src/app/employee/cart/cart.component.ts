import { Component, OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/shared/services/employee.service';
import { CartService } from 'src/app/shared/services/cart.service';
import { CartViewModel } from 'src/app/shared/models/cartviewmodel';
import { Item } from 'src/app/shared/models/item';
import { DomSanitizer } from '@angular/platform-browser';
import { Order } from 'src/app/shared/models/order';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { AdminService } from 'src/app/shared/services/admin.service';
import { ConfirmationDialogService } from 'src/app/shared/services/confirmation-dialogue.service';

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
  pointLimit: any;

  constructor(public cartservice: CartService, public employeeservice: EmployeeService,
    private domSanitizer: DomSanitizer, private toastr: ToastrService,
    private router: Router,private adminService:AdminService,
    private confirmationDialogService:ConfirmationDialogService
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

    this.adminService.getPointLimit().subscribe(
      data=>this.pointLimit=data
    )
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

    this.confirmationDialogService.confirm('Remove Item', 'Are you sure you want to remove this item from cart?','Remove','Cancel')
    .then((confirmed) => {
      if(confirmed){
        console.log('User confirmed:', confirmed);
        this.cartservice.deletefromcart(id).subscribe();
        window.location.reload();
      }
      else{
        console.log('User confirmed:', confirmed);
      }
    })
    .catch(() => console.log('User dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
  }

  async onCheckOut() {
    if(this.totalPoints<=this.pointLimit){
    if (this.totalPoints <= this.currentPoints) {
      this.itemQuantity.forEach((item, key) => {
          var Item=this.ItemList.find(x=>x.Id==key);
          if (Item.Quantity < item) {
            this.checkQuantity = 0;
            this.toastr.error(Item.Name + " only " + Item.Quantity + " left");
          }
      },
      )

      
        this.order.DateOfDelivery = null,
        this.order.DateOfOrder = new Date().toLocaleDateString(),
        this.order.Points = this.totalPoints,
        this.order.StatusDescriptionId = 1,
        this.order.UserId = Number(sessionStorage.getItem('userid'));

      if (this.checkQuantity == 1) {
          this.confirmationDialogService.confirm('Are You Sure?', 'Do you really want to Checkout from cart?','Checkout','Cancel')
          .then((confirmed) => {
            if(confirmed){
              console.log('User confirmed:', confirmed);
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
            else{
              console.log('User confirmed:', confirmed);
            }
          })
          .catch(() => console.log('User dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));

      }
    } else {
      this.toastr.error('Not Enough Points');
    }
  }
  else{
    this.toastr.error("Point limit exceeded");
  }
  }
}
