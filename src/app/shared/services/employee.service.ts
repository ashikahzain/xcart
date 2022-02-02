import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Item } from 'src/app/shared/models/item'
import { Observable } from 'rxjs';
import { Points } from 'src/app/shared/models/point'

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  itemList: Item[];
  id: number;
  currentpoint: number;

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
}
