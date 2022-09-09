import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserActiveInfo } from 'src/app/models/userActiveInfo.model';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class HeartbeatService {
  constructor(private http: HttpClient) {}

  baseUrl = 'https://localhost:7113/api/Cache/';

  notifyActive(userActiveInfo: UserActiveInfo): Observable<any> {
    return this.http.post<UserActiveInfo>(
      this.baseUrl + 'activeNotifying',
      userActiveInfo
    );
  }
}
