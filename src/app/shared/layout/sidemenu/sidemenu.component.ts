import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-sidemenu',
  templateUrl: './sidemenu.component.html',
  styleUrls: ['./sidemenu.component.css']
})
export class SidemenuComponent implements OnInit {

  filter: string;

  constructor(private authservice: AuthService, private router: Router) { }
  userName = sessionStorage.getItem('username');
  role = sessionStorage.getItem('role');
 
  ngOnInit(): void {
    console.log(this.userName);
    console.log(this.role);
  }
  public classToggled = false;

  public toggleField(): void {
    this.classToggled = !this.classToggled;

  }

  employeeswitch() {
    this.router.navigateByUrl('admin/home');
  }
  
  logout(): void {
    this.authservice.logout();
  }



}
