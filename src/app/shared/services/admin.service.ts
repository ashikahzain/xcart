import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Item } from '../models/item';
import { Order } from '../models/order';
import { User } from '../models/user';
import {AllEmployeePoints} from '../models/AllEmployeePoint';
import{AwardHistory} from '../models/AwardHistory';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  orderList: Order[];
  employeePointList:AllEmployeePoints[];
  trendingItemList :Item[];
  awardHistory:AwardHistory[];
  EmployeeDetails:User;

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

  getAllEmployeesPoints(){
    this.httpClient.get(environment.apiUrl+"/api/employees").toPromise().then(Response=>
      this.employeePointList=Response as AllEmployeePoints[]);
  }

  getAwardHistory(userId:number){
    this.httpClient.get(environment.apiUrl+"/api/employees/awards/"+userId).toPromise().then(Response=>
      this.awardHistory=Response as AwardHistory[]);
  }

  GetEmployee(UserId:number){
    this.httpClient.get(environment.apiUrl+"/api/employees/"+UserId).toPromise().then(Response=>
      this.EmployeeDetails=Response as User);
  }

}
