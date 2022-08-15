import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { UserLogin } from 'src/app/models/userlogin.model';
import { ToastrService } from 'ngx-toastr';
import { HttpRequest } from '@angular/common/http';
import { UserActiveInfo } from 'src/app/models/userActiveInfo.model';
import { HeartbeatService } from 'src/app/services/Auth/heartbeat.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  userLogin: UserLogin = {
    name: '',
    password: '',
  };
  userActiveInfo: UserActiveInfo = {
    key: '',
    value: '',
  };

  constructor(
    private authService: AuthService,
    private router: Router,
    private heartBeatService: HeartbeatService,
    private toastr: ToastrService
  ) {}

  loginForm = new FormGroup({
    userName: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
  });

  ngOnInit(): void {}

  onSubmit() {
    if (this.loginForm.invalid) return;

    this.userLogin.name = this.loginForm.get('userName')?.value;
    this.userLogin.password = this.loginForm.get('password')?.value;

    this.userActiveInfo.key = this.loginForm.get('userName')?.value;
    this.userActiveInfo.value = this.loginForm.get('userName')?.value;
    this.authService.signIn(this.userLogin).subscribe(
      (response) => {
        this.toastr.success('Login Successful');
        const token = (<any>response).token;
        const refreshToken = (<any>response).refreshToken;

        this.heartBeatService.notifyActive(this.userActiveInfo).subscribe();
        
        localStorage.setItem('username', this.userLogin.name);

        localStorage.setItem('token', token);
        localStorage.setItem('refreshToken', refreshToken);
        console.log(response);
        this.router.navigate(['/profile']);
      },
      (err) => {
        this.toastr.error('Invalid user name/password', 'Login Failed');
      }
    );
  }

  get userName() {
    return this.loginForm.get('userName');
  }

  get password() {
    return this.loginForm.get('password');
  }
}
