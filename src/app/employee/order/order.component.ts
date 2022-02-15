import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PaginationService } from 'src/app/shared/services/pagination.service';
import { Order } from 'src/app/shared/models/order';
import { EmployeeService } from 'src/app/shared/services/employee.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {

  //declaring variables
  filter: string;
  closeResult: string;
  UserId: number;
  orderList: Order[];
  TotalOrders: number;
  pagenumber: any = 1;
  pagesize: number = 5;
  paginationdata: number;
  exactPageList: number;
  pageField: any[];
  pageNo: boolean[] = [];

  constructor(private router: Router, private modalService: NgbModal, public employeeService: EmployeeService,
    public paginationService: PaginationService) { }

  ngOnInit(): void {
    this.paginationService.temppage = 0;
    this.pageNo[0] = true;
    this.UserId = Number(sessionStorage.getItem('userid'));
    this.GetEmployeeOrder();
  }

  //Get all the order of a particular User
  GetEmployeeOrder() {
    this.employeeService.getOrderByEmployeeId(this.UserId, this.pagenumber, this.pagesize).subscribe(
      data => {
        this.orderList = data;
        this.GetEmployeeOrderCount();
      });
  }

  //Get the count of orders of a user
  GetEmployeeOrderCount() {
    this.employeeService.getEmployeeOrderCount(this.UserId).subscribe(data => {
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

  showOrdersByPageNumber(page, i) {
    this.orderList = [];
    this.pageNo = [];
    this.pageNo[i] = true;
    this.pagenumber = page;
    this.GetEmployeeOrder();
  }

  //to open the poup
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
      this.employeeService.getOrderDetailsByOrderId(orderId);
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
}
