import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor() {}
  

 intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
  
  let token=sessionStorage.getItem('jwtToken');
  console.log("beforetoken");
  console.log(token);

  if(token){
    console.log("inside if");
    request=request.clone({
       setHeaders:{
         Authorization:`Bearer ${token}`
       }
     })
  }

    return next.handle(request);
  }
}
