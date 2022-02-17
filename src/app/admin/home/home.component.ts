import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/shared/services/admin.service';
import { AuthService } from 'src/app/shared/services/auth.service';
import { EmployeeService } from 'src/app/shared/services/employee.service';
import { MostAwarded } from 'src/app/shared/models/MostAwarded'
import { Item } from 'src/app/shared/models/item';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { Order } from 'src/app/shared/models/order';
import { PaginationService } from 'src/app/shared/services/pagination.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  filter: string;
  employee: MostAwarded;
  trendingItemList: Item;
  orderList: Order[];
  TotalOrders: number;
  pagenumber: any = 1;
  pagesize: number = 6;
  paginationdata: number;
  exactPageList: number;
  pageField: any[];
  pageNo: boolean[] = [];


  constructor(public adminService: AdminService, private authservice: AuthService,
    public employeeservice: EmployeeService, private domSanitizer: DomSanitizer, private router: Router,
    public paginationService: PaginationService) { }

  ngOnInit(): void {
    //pagination intialisation
    this.pageNo[0] = true;
    this.paginationService.temppage = 0;
    this.GetAllOrders();

    //the most rewarded employee
    this.employeeservice.getMostAwardedEmployee().subscribe(data => {
      this.employee = data;
      console.log(data);
    });

    //get top 2 trending items
    this.adminService.getTrendingItems().subscribe(data => {
      this.trendingItemList = data
      data.forEach(item => {
        item.Image = this.domSanitizer.bypassSecurityTrustUrl('data:image/jpg;base64,' + item.Image)
      });
    }
    );
  }

  //get all orders by pagination
  GetAllOrders() {
    this.adminService.getOrder(this.pagenumber, this.pagesize).subscribe(
      data => {
        this.orderList = data;
        this.GetOrderCount();
      }
    );
  }
  //total number of orders
  GetOrderCount() {
    this.adminService.getOrderCount().subscribe(data => {
      this.TotalOrders = data;
      this.TotalNumberofPages()
    });
  }
  
  //number of pages
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

  //pagination function 
  showOrdersByPageNumber(page, i) {
    this.orderList = [];
    this.pageNo = [];
    this.pageNo[i] = true;
    this.pagenumber = page;
    this.GetAllOrders();

  }

}
