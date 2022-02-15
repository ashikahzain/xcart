import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Item } from '../models/item';
import { Order } from '../models/order';
import { OrderDetails } from '../models/OrderDetails'
import { User } from '../models/user';
import { AllEmployeePoints } from '../models/AllEmployeePoint';
import { AwardHistory } from '../models/AwardHistory';
import { PointLimit } from '../models/pointlimit';
import { Award } from '../models/award';
@Injectable({
  providedIn: 'root'
})
export class AdminService {
  orderList: Order[];
  employeePointList: AllEmployeePoints[];
  trendingItemList: Item[];
  orderDetails: OrderDetails[];
  awardHistory: AwardHistory[];
  EmployeeDetails: User;
  awardList: Award[];
  itemList: Item;

  constructor(public httpClient: HttpClient) { }

  // get orders using viewmodel
  getOrder(pagenumber: number, pagesize: number): Observable<any> {
    return this.httpClient.get(environment.apiUrl + '/api/orders/?pagenumber=' + pagenumber + '&pagesize=' + pagesize);
  }
  //get top 2 trending item
  getTrendingItems(): Observable<any> {
    return this.httpClient.get(environment.apiUrl + '/api/orders/trending-item');
  }
  //add item 
  addItem(item: Item): Observable<any> {
    return this.httpClient.post(environment.apiUrl + '/api/items/', item);
  }

  //get item details by item id
  getItembyId(id: number): Observable<any> {
    return this.httpClient.get(environment.apiUrl + '/api/items/' + id);
  }
  //update item details
  updateItem(item: Item): Observable<any> {
    return this.httpClient.put(environment.apiUrl + '/api/items', item);
  }
  //delete an item from catalogue
  deleteItem(id: number): Observable<any> {
    return this.httpClient.get(environment.apiUrl + '/api/items/delete-item/' + id)
  }
  //get points of all employees
  getAllEmployeesPoints(pagenumber: number, pagesize: number): Observable<any> {
    return this.httpClient.get(environment.apiUrl + "/api/employees?pagenumber=" + pagenumber + '&pagesize=' + pagesize);
  }
  //get award history of an employee using user id
  getAwardHistory(userId: number): Observable<any> {
    return this.httpClient.get(environment.apiUrl + "/api/awardhistory/" + userId)
  }

  //get order details using order id
  getOrderDetailsByOrderId(orderId: number) {
    this.httpClient.get(environment.apiUrl + '/api/orders/' + orderId + '/order-details').toPromise().then(response =>
      this.orderDetails = response as OrderDetails[]);
  }

  //add award history
  addAwardHistory(award: AwardHistory): Observable<any> {
    return this.httpClient.post(environment.apiUrl + "/api/awardhistory", award);
  }

  //get employee name using user id
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

  //get award details using award id
  getAwardbyId(id: number): Observable<any> {
    return this.httpClient.get(environment.apiUrl + '/api/awards/' + id);
  }

  //update award details
  updateAward(award: Award): Observable<any> {
    return this.httpClient.put(environment.apiUrl + '/api/awards/update-award', award);
  }

  //delete an award
  deleteAward(id: number): Observable<any> {
    return this.httpClient.get(environment.apiUrl + '/api/awards/delete-award/' + id)
  }

  // Update Status
  ChangeStatus(order: Order): Observable<any> {
    return this.httpClient.put(environment.apiUrl + '/api/orders/change-status', order);
  }

  // Get Specified Order
  getSpecifiedOrder(statusId: number, pagenumber: number, pagesize: number): Observable<any> {
    return this.httpClient.get(environment.apiUrl + '/api/orders/status/' + statusId + '?pagenumber=' + pagenumber + '&pagesize=' + pagesize);
  }

  //get total number of employees
  getEmployeeCount(): Observable<any> {
    return this.httpClient.get(environment.apiUrl + "/api/employees/employee-count");
  }

  //get number of orders
  getOrderCount(): Observable<any> {
    return this.httpClient.get(environment.apiUrl + "/api/orders/order-count");
  }

  //number of orders in open and fulfilled
  getStatusOrderCount(id: number): Observable<any> {
    return this.httpClient.get(environment.apiUrl + "/api/orders/" + id + '/order-status-count');
  }

  //Point Limit
  getPointLimit(): Observable<any> {
    return this.httpClient.get(environment.apiUrl + "/api/points/point-limit");
  }

  //add point limit
  addPointLimit(pointlimit: PointLimit): Observable<any> {
    return this.httpClient.put(environment.apiUrl + "/api/points/point-limit", pointlimit);
  }
}
