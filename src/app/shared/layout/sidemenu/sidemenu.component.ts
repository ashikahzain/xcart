import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from '../../services/admin.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-sidemenu',
  templateUrl: './sidemenu.component.html',
  styleUrls: ['./sidemenu.component.css']
})
export class SidemenuComponent implements OnInit {

  filter: string;
  pointLimit: any;

  constructor(private authservice: AuthService, private router: Router, private adminService: AdminService) { }
  userName = sessionStorage.getItem('username');
  role = sessionStorage.getItem('role');

  ngOnInit(): void {
    console.log(this.userName);
    console.log(this.role);
    //get point limit
    this.adminService.getPointLimit().subscribe(
      data => this.pointLimit = data
    )

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
