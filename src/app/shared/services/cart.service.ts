import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Cart } from '../models/cart';
import { Order } from '../models/order';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  userId: string; 
  cart:Cart[];
  constructor(private httpClient: HttpClient) { }

  getAllCart():Observable<any>{
    this.userId=sessionStorage.getItem('userid');
    return this.httpClient.get(environment.apiUrl+"/api/cart/cart/"+this.userId);
  }

  increaseQuantity(id:number):Observable<any>{
    return this.httpClient.get(environment.apiUrl+"/api/cart/increase-quantity/"+id);
  }

  decreaseQuantity(id:number):Observable<any>{
    return this.httpClient.get(environment.apiUrl+"/api/cart/decrease-quantity/"+id);
  }
  deletefromcart(id:number):Observable<any>{
    return this.httpClient.delete(environment.apiUrl + '/api/cart/'+id);
  }

  placeOrder(order:Order):Observable<any>{
    return this.httpClient.post(environment.apiUrl + '/api/orders',order);
  }

  placeOrderFromCart(order:Order):Observable<any>{
    return this.httpClient.post(environment.apiUrl + '/api/orders/cart',order);
  }

  deletefromCartbyUserId(id:number):Observable<any>{
    return this.httpClient.delete(environment.apiUrl + '/api/cart/delete-cart/'+id);
  }

  compareQuantity(id:number):Observable<any>{
    return this.httpClient.get(environment.apiUrl + '/api/orders/quantity-check/'+id);
  }
}
