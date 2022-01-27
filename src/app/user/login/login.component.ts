import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  //declare variables
  loginForm : FormGroup;
  isSubmitted = false;
  //loginUser: Login = new Login();
  error = '';

  constructor() { }

  ngOnInit(): void {

    
  }

}
