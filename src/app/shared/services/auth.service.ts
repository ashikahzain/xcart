import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from 'src/app/shared/models/user'
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private httpClient: HttpClient) { }

  //Authorize return token with roleId and userName
  public loginVerify(user:User){
    //calling webservice url and passing username and password
    console.log("Attempt authenticate and authorize : :");
    console.log(user);
    return this.httpClient.get(environment.apiUrl+"/api/login/"+user.UserName+"/" + user.Password)
  }
}
