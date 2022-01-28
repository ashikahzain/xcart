import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Order } from '../models/order'
@Injectable({
  providedIn: 'root'
})
export class AdminService {

  orderList: Order[];

  constructor(public httpClient: HttpClient) {

   }

   //get order
   getOrder(){
     this.httpClient.get(environment.apiUrl+"api/order").toPromise().then(response =>
      this.orderList=response as Order[]);
   }

}
