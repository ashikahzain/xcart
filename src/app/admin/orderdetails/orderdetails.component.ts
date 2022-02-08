import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from 'src/app/shared/services/admin.service';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { Order } from 'src/app/shared/models/order';


@Component({
  selector: 'app-orderdetails',
  templateUrl: './orderdetails.component.html',
  styleUrls: ['./orderdetails.component.css']
})
export class OrderdetailsComponent implements OnInit {

  filter:string;
  closeResult:string;
  
  constructor(public adminService:AdminService, private router:Router,
    private modalService: NgbModal) { }

  ngOnInit(): void {
    this.adminService.getOrder();
  }
  AdminHome(){
    this.router.navigateByUrl('admin/home')
  }
  All(){
    this.adminService.getOrder();
  }
  Open(){
    this.adminService.getSpecifiedOrder(1);
  }
  FulFilled(){
    this.adminService.getSpecifiedOrder(2);
  }

  open(content,orderId:number) {
    this.modalService.open(content,
   {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = 
         `Dismissed ${this.getDismissReason(reason)}`;
    });
    console.log(orderId);
    if(orderId!=0 || orderId!=null){
      console.log("Hi");
      this.adminService.getOrderDetailsByOrderId(orderId);
    }

  }
  
  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }

  UpdateStatus(order:Order){
    if(order.Id!=0 || order.Id!=null){
      this.adminService.ChangeStatus(order).subscribe(
        data=>{
          console.log(data);
          location.reload();
          
        }
      )
    }

  }
}
