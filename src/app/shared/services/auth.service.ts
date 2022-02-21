import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from 'src/app/shared/models/user';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private httpClient: HttpClient, private router: Router) { }

  // Authorize return token with roleId and userName
  public loginVerify(user: User): any {
    // calling webservice url and passing username and password
    console.log('Attempt authenticate and authorize :');
    console.log(user);
    return this.httpClient.post(environment.apiUrl + '/api/login', user);
  }

    //get by email id
    getbyEmailId(email:string):Observable<any>{
      return this.httpClient.get(environment.apiUrl+ '/api/login?email='+email);
    }
  // logout
  public logout(): void {
    localStorage.clear();
    sessionStorage.clear();
    this.router.navigateByUrl('');
  }
}
