import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { JwtResponse } from 'src/app/shared/models/jwtresponse';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  // declare variables
  loginForm: FormGroup;
  isSubmitted = false;
  error = '';
  jwtResponse: any = new JwtResponse;

  constructor(private formBuilder: FormBuilder, private router: Router, private authService: AuthService) { }

  ngOnInit(): void {
    // FormGroup Declaration
    this.loginForm = this.formBuilder.group(
      {
        UserName: ['', [Validators.required, Validators.minLength(2)]],
        Password: ['', [Validators.required]]
      }
    );
  }

  // Get Controls
  get formControls(): any {
    return this.loginForm.controls;
  }

  // login verify credentials
  login(): void {
    this.isSubmitted = true;
    console.log(this.loginForm.value);

    // invalid
    if (this.loginForm.invalid)
      return;

    // valid
    if (this.loginForm.valid) {

      // calling method from AuthService -Authorization
      this.authService.loginVerify(this.loginForm.value)
        .subscribe(data => {
          console.log(data);
          // token with role and username
          this.jwtResponse = data;

          // either local/session
          sessionStorage.setItem('jwtToken', this.jwtResponse.Token);
          const token = sessionStorage.getItem('jwtToken');

          if (this.jwtResponse.Token != null) {
            // logged as Admin
            console.log('Successfully logged in');
            // storing in localStorage/sessionStorage
            localStorage.setItem('username', this.jwtResponse.UserName);
            sessionStorage.setItem('username', this.jwtResponse.UserName);
            sessionStorage.setItem('role', this.jwtResponse.RoleName);
            sessionStorage.setItem('userid', this.jwtResponse.UserId);

            console.log(this.jwtResponse.RoleName);
            if (this.jwtResponse.RoleName === 'Admin') {
              this.router.navigateByUrl('/admin/home');
            }
            else if (this.jwtResponse.RoleName === 'Employee') {
              this.router.navigateByUrl('/employee/home');
            }
          }
          else {
            this.error = 'Sorry Not allowed. Invalid authorization';
          }
        },
          error => {
            this.error = 'Invalid Username or Password. Try Again!';
          }
        );
    }
  }

}
