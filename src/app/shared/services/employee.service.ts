import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Item } from 'src/app/shared/models/item'
import { Points } from 'src/app/shared/models/point'
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  itemList: Item[];
id: number;
currentpoint:Points[];

  constructor(private httpClient: HttpClient) { }

  // Get all Items 
  getItems() {
    this.httpClient.get(environment.apiUrl + '/api/items').toPromise().then(response =>
      this.itemList = response as Item[]);
  }

  getCurrentPoint(){
  this.id=parseInt(sessionStorage.getItem('userid'));
  this.httpClient.get(environment.apiUrl +'/api/points/'+this.id).toPromise().then(response =>
    this.currentpoint = response as Points[]);

    console.log("Inside Service"+this.currentpoint);

  }
}
