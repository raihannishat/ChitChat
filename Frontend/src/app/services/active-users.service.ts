import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ActiveUser } from '../models/activeUser';

@Injectable({
  providedIn: 'root',
})
export class ActiveUsersService {
  constructor(private http: HttpClient) {}

  baseUrl = 'https://localhost:7113/api/Cache/';

  activeUsersList(): Observable<any> {
    return this.http.get<ActiveUser>(this.baseUrl + 'activeUsers');
  }
}
