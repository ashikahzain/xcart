import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from 'src/app/shared/services/admin.service';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { Order } from 'src/app/shared/models/order';
import { PaginationService } from 'src/app/shared/services/pagination.service';
import { ConfirmationDialogService } from 'src/app/shared/services/confirmation-dialogue.service';

@Component({
  selector: 'app-orderdetails',
  templateUrl: './orderdetails.component.html',
  styleUrls: ['./orderdetails.component.css']
})
export class OrderdetailsComponent implements OnInit {

  //declaring variables
  filter: string;
  closeResult: string;
  TotalOrders: number;
  pagenumber: any = 1;
  pagesize: number = 4;
  paginationdata: number;
  exactPageList: number;
  pageField: any[];
  pageNo: boolean[] = [];
  orderList: Order[];
  all:boolean= false;
  opened:boolean = false;
  fulfilled:boolean = false;

  constructor(public adminService: AdminService, private router: Router,
    private modalService: NgbModal, public paginationService: PaginationService,
    private confirmationDialogService:ConfirmationDialogService ) { }

  ngOnInit(): void {

    this.pageNo[0] = true;

    //Get all orders whose order status is Open
    this.Open();
  }

  //to navigate to the home page on clicking home button
  AdminHome() {
    this.router.navigateByUrl('admin/home')
  }

  //To get all orders
  All() {
    this.all = true;
    this.opened = false;
    this.fulfilled = false;
    this.paginationService.temppage = 0;
    this.adminService.getOrder(this.pagenumber, this.pagesize).subscribe(
      data => {
        this.orderList = data;
        this.GetOrderCount();
      });
  }

  //Get all orders whose order status is Open
  Open() {
    this.all = false;
    this.opened = true;
    this.fulfilled = false;
    this.paginationService.temppage = 0;
    this.adminService.getSpecifiedOrder(1,this.pagenumber, this.pagesize).subscribe(
      data => {
        this.orderList = data;
        this.GetOrderStatusCount(1);
      });
  }

  //Get all orders whose order status is Fulfilled
  FulFilled() {
    this.all = false;
    this.opened = false;
    this.fulfilled = true;
    this.paginationService.temppage = 0;
    this.adminService.getSpecifiedOrder(2,this.pagenumber, this.pagesize).subscribe(
      data => {
        this.orderList = data;
        this.GetOrderStatusCount(2);
      });
  }

  //To get the total count of orders
  GetOrderCount() {
    this.adminService.getOrderCount().subscribe(data => {
      this.TotalOrders = data;
      this.TotalNumberofPages()
    });
  }

  //To get the total count of orders based on status
  GetOrderStatusCount(id: number) {
    this.adminService.getStatusOrderCount(id).subscribe(data => {
      this.TotalOrders = data;
      this.TotalNumberofPages();
    });
  }

  TotalNumberofPages() {
    this.paginationdata = (this.TotalOrders / this.pagesize);
    let tempPageData = this.paginationdata.toFixed();
    if (Number(tempPageData) < this.paginationdata) {
      this.exactPageList = Number(tempPageData) + 1;
      this.paginationService.exactPageList = this.exactPageList;
    }
    else {
      this.exactPageList = Number(tempPageData);
      this.paginationService.exactPageList = this.exactPageList
    }
    this.paginationService.pageOnLoad();
    this.pageField = this.paginationService.pageField;
  }


  showOrdersByPageNumberAll(page, i) {
    this.orderList = [];
    this.pageNo = [];
    this.pageNo[i] = true;
    this.pagenumber = page;
    this.All();
  }

  showOrdersByPageNumberOpen(page,i){
    this.orderList = [];
    this.pageNo = [];
    this.pageNo[i] = true;
    this.pagenumber = page;
    this.Open();
  }

  showOrdersByPageNumberFulfilled(page,i){
    this.orderList = [];
    this.pageNo = [];
    this.pageNo[i] = true;
    this.pagenumber = page;
    this.FulFilled();
  }

  //To open popup
  open(content, orderId: number) {
    this.modalService.open(content,
      { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {
        this.closeResult = `Closed with: ${result}`;
      }, (reason) => {
        this.closeResult =
          `Dismissed ${this.getDismissReason(reason)}`;
      });
    console.log(orderId);
    if (orderId != 0 || orderId != null) {
      console.log("Hi");
      this.adminService.getOrderDetailsByOrderId(orderId);
    }
  }

  //to close the popup
  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }

  //Function to update the status of an order
  UpdateStatus(order: Order) {
  //   if(confirm("Are you sure you want to change the status to fulfilled?")){
  //   if (order.Id != 0 || order.Id != null) {
  //     this.adminService.ChangeStatus(order).subscribe(
  //       data => {
  //         console.log(data);
  //         window.location.reload();
  //       });
  //   }
  // }
  this.confirmationDialogService.confirm('Are You Sure', 'Do you really want to change the Status to Fulfilled?','Ok','Cancel')
  .then((confirmed) => {
    if(confirmed){
      if (order.Id != 0 || order.Id != null) {
        this.adminService.ChangeStatus(order).subscribe(
          data => {
            console.log(data);
            window.location.reload();
          });
      }
    }
    else{
      console.log('User confirmed:', confirmed);
    }
  })
  .catch(() => console.log('User dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));

  }

}