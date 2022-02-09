import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Item } from 'src/app/shared/models/item'
import { Observable } from 'rxjs';
import { Points } from 'src/app/shared/models/point'
import { Order } from '../models/order';
import { OrderDetails } from '../models/OrderDetails';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  itemList: Item[];
  id: number;
  currentpoint: number;
  orderList: Order[];
  orderDetails: OrderDetails[];

  constructor(private httpClient: HttpClient) { }

  // Get all Items 
  getItems():Observable<any> {
    return this.httpClient.get(environment.apiUrl + '/api/items');
  }

  getCurrentPoints():Observable<any>{
    this.id = parseInt(sessionStorage.getItem('userid'));
    return this.httpClient.get(environment.apiUrl+"/api/points/"+this.id);
  }

  getMostAwardedEmployee():Observable<any>{
    return this.httpClient.get(environment.apiUrl+"/api/employees/most-awards");
  }

  getEmployeeProfile():Observable<any>{
    this.id = parseInt(sessionStorage.getItem('userid'));
    return this.httpClient.get(environment.apiUrl+"/api/employees/employee/"+this.id);
  }

  //get order details by employee id
  getOrderByEmployeeId(id:number): Observable<any>{
    return this.httpClient.get(environment.apiUrl + '/api/employees/OrderByEmpId/'+id);
  }

  getOrderDetailsByOrderId(orderId: number) {
    this.httpClient.get(environment.apiUrl + '/api/orders/GetOrderDetails/' + orderId).toPromise().then(response =>
      this.orderDetails = response as OrderDetails[]);
  }
}
