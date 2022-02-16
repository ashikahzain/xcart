import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  userRole:number
  expectedRole:string[]=[]
  constructor (private authService:AuthService,private router:Router){}
  canActivate(
    next: ActivatedRouteSnapshot): boolean {
       //get all roles
 
      //expected role vs current role
      //routes            //login
      this.expectedRole = next.data.role;
      let currentRole = sessionStorage.getItem('role');
    
      //check the condition

      if (!this.expectedRole.includes(currentRole)) {
        this.router.navigateByUrl('login');
        this.authService.logout()
        return false;
      }
      return true;
  }
  
}
