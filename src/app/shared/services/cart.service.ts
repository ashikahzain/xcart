import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  userId: string; 
  constructor(private httpClient: HttpClient) { }

  getAllCart():Observable<any>{
    this.userId=sessionStorage.getItem('userid');
    return this.httpClient.get(environment.apiUrl+"/api/cart/cart/"+this.userId);
  }

}
