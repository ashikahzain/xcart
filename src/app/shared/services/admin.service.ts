import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Item } from '../models/item';
import { Order } from '../models/order';
import { OrderDetails } from '../models/OrderDetails'
@Injectable({
  providedIn: 'root'
})
export class AdminService {

  orderList: Order[];
  trendingItemList :Item[];
  orderDetails:OrderDetails[];

  constructor(public httpClient: HttpClient) {

  }

  // get orders using viewmodel
  getOrder(): void {
    this.httpClient.get(environment.apiUrl + '/api/orders').toPromise().then(response =>
      this.orderList = response as Order[]);
  }

  getTrendingItems():Observable<any>{
   return this.httpClient.get(environment.apiUrl+'/api/orders/trending-item');
  }

  //get order details by item id
  getOrderDetailsByOrderId(orderId:number){
    this.httpClient.get(environment.apiUrl + '/api/orders/GetOrderDetails/'+orderId).toPromise().then(response =>
      this.orderDetails = response as OrderDetails[]);
  }

  //Get Order By Id
  GetOrderById(orderId:number):Observable<any>{
    return this.httpClient.get(environment.apiUrl +'/api/orders/GetOrderById/'+orderId);
  }

  // Update Status
  UpdateStatus(orderId:number):Observable<any>{
    console.log('Annie')
    return this.httpClient.put(environment.apiUrl + '/api/orders/change-status',orderId);
}
}
