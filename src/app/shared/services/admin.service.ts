import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Item } from '../models/item';
import { Order } from '../models/order';
import { OrderDetails } from '../models/OrderDetails'
import { User } from '../models/user';
import {AllEmployeePoints} from '../models/AllEmployeePoint';
import{AwardHistory} from '../models/AwardHistory';

import { Award } from '../models/award';
@Injectable({
  providedIn: 'root'
})
export class AdminService {

  orderList: Order[];
  employeePointList: AllEmployeePoints[];
  trendingItemList: Item[];
  orderDetails: OrderDetails[];
  awardHistory:AwardHistory[];
  EmployeeDetails:User;
  awardList: Award[];

  constructor(public httpClient: HttpClient) {

  }

  // get orders using viewmodel
  getOrder(): void {
    this.httpClient.get(environment.apiUrl + '/api/orders').toPromise().then(response =>
      this.orderList = response as Order[]);
  }

  getTrendingItems(): Observable<any> {
    return this.httpClient.get(environment.apiUrl + '/api/orders/trending-item');
  }

  getAwardHistory(userId:number):Observable<any>{
    return this.httpClient.get(environment.apiUrl+"/api/awardhistory/"+userId)
    //.toPromise().then(Response=>
      //this.awardHistory=Response as AwardHistory[]);
  //get order details by item id
  }

  getOrderDetailsByOrderId(orderId: number) {
    this.httpClient.get(environment.apiUrl + '/api/orders/GetOrderDetails/' + orderId).toPromise().then(response =>
      this.orderDetails = response as OrderDetails[]);
  }

  addItem(item: Item): Observable<any> {
    return this.httpClient.post(environment.apiUrl + '/api/items/', item);

  }

  getAllEmployeesPoints() {
    this.httpClient.get(environment.apiUrl + "/api/employees").toPromise().then(Response =>
      this.employeePointList = Response as AllEmployeePoints[]);
  }

  addAwardHistory(award: AwardHistory): Observable<any> {
    return this.httpClient.post(environment.apiUrl + "/api/awardhistory", award);
  }

  GetEmployee(UserId: number) {
    this.httpClient.get(environment.apiUrl + "/api/employees/" + UserId).toPromise().then(Response =>
      this.EmployeeDetails = Response as User);

  }
  // Get all Awards
  getAwards(): void {
    this.httpClient.get(environment.apiUrl + '/api/awards').toPromise().then(response =>
      this.awardList = response as Award[]);;
  }
  //Add a new award
  addAward(award: Award): Observable<any> {
    return this.httpClient.post(environment.apiUrl + '/api/awards', award);
  }

  // Update Status
  ChangeStatus(order: Order): Observable<any> {
    console.log('Annie')
    return this.httpClient.put(environment.apiUrl + '/api/orders/change-status', order);
  }

  // Get Specified Order
  getSpecifiedOrder(statusId:number): void {
    this.httpClient.get(environment.apiUrl + '/api/orders/status/'+statusId).toPromise().then(response =>
      this.orderList = response as Order[]);
  }
}
