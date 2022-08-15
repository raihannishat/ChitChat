import { Injectable } from '@angular/core';
import { UsersService } from './users.service';
import { UserLogin } from '../models/userlogin.model';
import { HttpClient, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RefreshToken } from '../models/refreshToken.model';
import { User } from '../models/user.model';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  jwt = new JwtHelperService();
  private decodedTokenObj;

  constructor(private http: HttpClient) {
    this.decodedTokenObj = new DecodedToken();
  }

  baseUrl = 'https://localhost:7113/api/Auth/';

  signUp(user: User): Observable<User> {
    return this.http.post<User>(this.baseUrl + 'signup', user);
  }

  signIn(userlogin: UserLogin) {
    return this.http.post<string>(this.baseUrl + 'signin', userlogin);
  }

  refreshToken(refreshTokenModel: RefreshToken): Observable<any> {
    return this.http.post<any>(
      this.baseUrl + 'refreshtoken',
      refreshTokenModel
    );
  }
  getAccessToken() {
    return localStorage.getItem('token')?.toString();
  }

  getRefreshToken() {
    return localStorage.getItem('refreshToken')?.toString();
  }

  saveAccessToken(token: string) {
    localStorage.removeItem('token');
    localStorage.setItem('token', token);
  }

  saveRefreshToken(refreshToken: string) {
    localStorage.removeItem('refreshToken');
    localStorage.setItem('refreshToken', refreshToken);
  }

  logOut() {
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
  }

  isAuthenticated(): boolean {
    const token = localStorage.getItem('token');
    if (token != null) return true;
    return false;
  }

  addTokenHeader(req: HttpRequest<any>) {
    let token: string | null = localStorage.getItem('token');
    if (token) {
      req = req.clone({
        headers: req.headers.set('Authorization', 'Bearer ' + token),
      });
    }
    return req;
  }

  decodeToken() {
    let accessToken = JSON.stringify(localStorage.getItem('token'));
    let decodeToken = JSON.stringify(this.jwt.decodeToken(accessToken));
    this.decodedTokenObj = JSON.parse(decodeToken);
    return this.decodedTokenObj;
  }
}

export class DecodedToken {
  sub: string = '';
  exp: number = 0;
}
