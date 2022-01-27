import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { JwtResponse } from 'src/app/shared/models/jwtresponse'
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/shared/services/auth.service'

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
  jwtResponse: any = new JwtResponse;

  constructor(private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthService) { }

  ngOnInit(): void {
    //FormGroup
    this.loginForm = this.formBuilder.group(
      {
        Username: ['', [Validators.required, Validators.minLength(2)]],
        Password: ['', [Validators.required]]
      }
    );
  }

  //Get Controls
  get formControls() {
    return this.loginForm.controls;
  }

  //login verify credentials
  login() {

    this.isSubmitted = true;
    console.log(this.loginForm.value);

    //invalid
    if (this.loginForm.invalid)
      return;

    //valid
    if (this.loginForm.valid) {

      //calling method from AuthService -Authorization
      this.authService.loginVerify(this.loginForm.value)
        .subscribe(data => {
          console.log(data);
          //token with roleid and name
          this.jwtResponse = data;

          //either local/session
          sessionStorage.setItem("jwtToken", this.jwtResponse.token);

          if (this.jwtResponse.token != null) {
            //logged as Admin
            console.log("Successfully logged in");
            //storing in localStorage/sessionStorage
            localStorage.setItem("username", this.jwtResponse.UserName);
            sessionStorage.setItem("username", this.jwtResponse.UserName);
            this.router.navigateByUrl('/employee/home');
          }
          else {
            this.error = "Sorry Not allowed. Invalid authorization"
          }
        },
          error => {
            this.error = "Invalid Username or Password. Try Again!"
          }
        );
    }
  }

}
