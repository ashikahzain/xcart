import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-sidemenu',
  templateUrl: './sidemenu.component.html',
  styleUrls: ['./sidemenu.component.css']
})
export class SidemenuComponent implements OnInit {

  constructor(private authservice: AuthService) { }
  userName = sessionStorage.getItem('username');
  ngOnInit(): void {

  }
  classToggled = false;

  public toggleField(): void {
    this.classToggled = !this.classToggled;
  }

  logout(): void {
    this.authservice.logout();
  }


}
