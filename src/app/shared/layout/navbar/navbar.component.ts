import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(private authservice: AuthService, public router: Router) { }

  ngOnInit(): void {
  }
  logout(): void {
    this.authservice.logout();
  }

  employeeswitch() {
    this.router.navigateByUrl('employee/home');
  }
}
