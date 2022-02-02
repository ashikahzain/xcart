import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Item } from 'src/app/shared/models/item'
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  itemList: Item[];
  constructor(private httpClient: HttpClient) { }

  // Get all Items 
  getItems():Observable<any> {
    return this.httpClient.get(environment.apiUrl + '/api/items');
  }
}
