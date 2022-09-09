import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { delay, map, Observable, of } from 'rxjs';
import { User } from '../models/user.model';
import { UserLogin } from '../models/userlogin.model';
@Injectable({
  providedIn: 'root',
})
export class UsersService {
  constructor(private http: HttpClient) {}

  baseUrl = 'https://localhost:7113/api/User/';

  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl);
  }

  updateUser(id: string, user: User) {
    return this.http.put<User>(this.baseUrl + id, user);
  }

  deleteUser(id: string) {
    return this.http.delete<User>(this.baseUrl + id);
  }

  checkIfUsernameExists(value: string) {
    return this.getUserbyName(value).pipe(
      delay(500),
      map((result) => (result ? { usernameAlreadyExists: true } : null))
    );
  }
  checkIfEmailExists(value: string) {
    return this.getUserbyEmail(value).pipe(
      delay(500),
      map((result) => (result ? { emailAlreadyExists: true } : null))
    );
  }

  getUserbyName(value: string): Observable<boolean> {
    const val = this.http.get<boolean>(
      this.baseUrl + 'GetByName' + '/' + value
    );
    return val;
  }

  getUserbyEmail(value: string): Observable<boolean> {
    const val = this.http.get<boolean>(
      this.baseUrl + 'GetByEmail' + '/' + value
    );
    return val;
  }
}
