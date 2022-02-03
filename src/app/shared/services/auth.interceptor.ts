import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private router: Router, private authservice: AuthService, public toastr: ToastrService) { }


  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const token = sessionStorage.getItem('jwtToken');

    if (token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`,
        }
      });
    }
    else {
      request = request.clone({
        setHeaders: {
          ApiKey: 'SecretKey'
        }
      });
    }
    return next.handle(request)
      .pipe(catchError(
        (error: HttpErrorResponse) => {
          if (error instanceof HttpErrorResponse) {
            if (error.error instanceof ErrorEvent) {
              console.error("Error Event");
            } else {
              console.log(`error status : ${error.status} ${error.statusText}`);
              switch (error.status) {
                case 401:      //login
                  console.log('Unauthorized');
                  this.authservice.logout();
                  this.toastr.error('Unauthorized');
                  break;
                case 403:     //forbidden
                  console.log('Access Denied');
                  this.toastr.error('Access Denied');
                  break;
                case 404: //not found
                  console.log('Not found');
                  this.toastr.error('Not Found');
                case 400:
                  console.log('Bad Request');
                  this.toastr.error('Bad Request');
              }
            }
          } else {
            console.error("Error occured");
          }
          return throwError(error);
        })
      );


  }
}
