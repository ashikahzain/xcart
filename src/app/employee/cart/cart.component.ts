import { Component, OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/shared/services/employee.service';
import { CartService } from 'src/app/shared/services/cart.service';
import { CartViewModel } from 'src/app/shared/models/cartviewmodel'
import { Item } from 'src/app/shared/models/item';
import { DomSanitizer } from '@angular/platform-browser';
import { Order } from 'src/app/shared/models/order';
import { ToastrService } from 'ngx-toastr';
import { AdminService } from 'src/app/shared/services/admin.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  currentPoints: number;
  cart: CartViewModel;
  totalPoints: number = 0;
  order = new Order();
  itemQuantity = new Map<number, number>();
  checkQuantity: number = 1;
  ItemList:Item[]=[];

  constructor(public cartservice: CartService, public employeeservice: EmployeeService, private domSanitizer: DomSanitizer, private toastr: ToastrService, private adminService: AdminService) { }

  ngOnInit(): void {
    this.employeeservice.getItems().subscribe(data=>{
      this.ItemList=data;
    });

    this.cartservice.getAllCart().subscribe(
      data => {
        console.log(data);
        this.cart = data;
        data.forEach(item => {
          item.ItemImage = this.domSanitizer.bypassSecurityTrustUrl('data:image/jpg;base64,' + item.ItemImage),
            this.totalPoints += item.Quantity * item.ItemPoints;
          this.itemQuantity.set(item.ItemId, item.Quantity);
          console.log(this.itemQuantity);
        });
      }
    );

 

    this.employeeservice.getCurrentPoints().subscribe(
      data => {
        this.currentPoints = data;
      });

  
  }

  


  /*
  compareItemQuantity():any{
    this.cartservice.compareQuantity(Number(sessionStorage.getItem('userid'))).subscribe(
      data=>{console.log(data)
      return data;
      }
    )
  }
  */
  onDecrease(id: number) {
    this.cartservice.decreaseQuantity(id).subscribe();
    window.location.reload();
  }

  onIncrease(id: number) {
    this.cartservice.increaseQuantity(id).subscribe();
    window.location.reload();
  }

  deletefromCart(id: number) {
    if (confirm("Do you want to delete this item from cart?")) {
      this.cartservice.deletefromcart(id).subscribe();
    }
    window.location.reload();
  }
  //on checkout function
  async onCheckOut() {
    if (this.totalPoints <= this.currentPoints) {

      this.itemQuantity.forEach((item, key) => {
         /*this.adminService.getItembyId(key).subscribe(
          product => {
            if (product.Quantity < item) {
              this.checkQuantity = 0;
              console.log("inside loop");
              console.log(this.checkQuantity);
              this.toastr.error(product.Name + " only " + product.Quantity + " left");
            }
          }
        )*/
          //console.log(this.itemQuantity);
          var Item=this.ItemList.find(x=>x.Id==key);
          //console.log(Item);
          if (Item.Quantity < item) {
            this.checkQuantity = 0;
            //console.log("inside loop");
            //console.log(this.checkQuantity);
            this.toastr.error(Item.Name + " only " + Item.Quantity + " left");
          }
      },
      )
      //await new Promise(f => setTimeout(f, 500));
      
        this.order.DateOfDelivery = null,
        this.order.DateOfOrder = new Date().toLocaleDateString(),
        this.order.Points = this.totalPoints,
        this.order.StatusDescriptionId = 1,
        this.order.UserId = Number(sessionStorage.getItem('userid'));

        console.log(this.order);
        console.log(this.checkQuantity);

      if (this.checkQuantity == 1) {
        if (confirm('Are you sure you want place the order?')) {
          this.cartservice.placeOrderFromCart(this.order).subscribe(data => {
            console.log(data)
          });
        }

      }
      //window.location.reload();
    }
    
    else {
      this.toastr.error('Not Enough Points');
    }


  }
    
}

