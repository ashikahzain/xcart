import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Item } from '../models/item';
import { Order } from '../models/order';
@Injectable({
  providedIn: 'root'
})
export class AdminService {

  orderList: Order[];
  trendingItemList :Item[];

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
addItem(item: Item): Observable<any> {
  return this.httpClient.post(environment.apiUrl +'/api/items/', item);

}

}
