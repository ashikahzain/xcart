import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Item } from 'src/app/shared/models/item'
@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  itemList: Item[]
  constructor(private httpClient: HttpClient) { }

  // Get all Items 
  getItems() {
    this.httpClient.get(environment.apiUrl + '/api/items').toPromise().then(response =>
      this.itemList = response as Item[]);
  }
}
