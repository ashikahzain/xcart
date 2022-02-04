import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Item } from '../models/item';
import { Order } from '../models/order';
import { Award } from '../models/award';
@Injectable({
  providedIn: 'root'
})
export class AdminService {

  orderList: Order[];
  trendingItemList :Item[];
  awardList: Award[];

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

  // Get all Awards
  getAwards():Observable<any> {
    return this.httpClient.get(environment.apiUrl + '/api/awards');
  }

}
