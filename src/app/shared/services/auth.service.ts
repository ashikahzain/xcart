import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from 'src/app/shared/models/user'
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private httpClient: HttpClient,private router:Router) { }

  //Authorize return token with roleId and userName
  public loginVerify(user:User){
    //calling webservice url and passing username and password
    console.log("Attempt authenticate and authorize : :");
    console.log(user);
    return this.httpClient.post(environment.apiUrl+"/api/login/"+user.UserName+"/" + user.Password,null)
  }

  public logout(){
    localStorage.clear();
    sessionStorage.clear();
    this.router.navigateByUrl('');
  }
}
