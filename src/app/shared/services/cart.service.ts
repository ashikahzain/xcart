import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Cart } from '../models/cart';
import { Order } from '../models/order';
import { OrderDetails } from '../models/OrderDetails';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  userId: string;
  cart: Cart[];
  constructor(private httpClient: HttpClient) { }

  //get items in cart of an employee using user id
  getAllCart(): Observable<any> {
    this.userId = sessionStorage.getItem('userid');
    return this.httpClient.get(environment.apiUrl + "/api/cart/cart/" + this.userId);
  }

  //increase quantity of an item in cart
  increaseQuantity(id: number): Observable<any> {
    return this.httpClient.get(environment.apiUrl + "/api/cart/increase-quantity/" + id);
  }
  //decrease quantity of an item in cart
  decreaseQuantity(id: number): Observable<any> {
    return this.httpClient.get(environment.apiUrl + "/api/cart/decrease-quantity/" + id);
  }
  //delete an item from cart
  deletefromcart(id: number): Observable<any> {
    return this.httpClient.delete(environment.apiUrl + '/api/cart/' + id);
  }
  //place order on direct buy
  placeOrder(order: Order): Observable<any> {
    return this.httpClient.post(environment.apiUrl + '/api/orders', order);
  }

  //place order from cart
  placeOrderFromCart(order: Order): Observable<any> {
    return this.httpClient.post(environment.apiUrl + '/api/orders/cart', order);
  }

  //empty cart of a particular user
  deletefromCartbyUserId(id: number): Observable<any> {
    return this.httpClient.delete(environment.apiUrl + '/api/cart/delete-cart/' + id);
  }

  //add order details on buying a product directly
  updateOrderDetails(orderdetails: OrderDetails): Observable<any> {
    return this.httpClient.post(environment.apiUrl + '/api/orders/order-details', orderdetails);
  }
}
