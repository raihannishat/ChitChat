import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subscription, switchMap, timer } from 'rxjs';
import { UserActiveInfo } from './models/userActiveInfo.model';
import { AuthService } from './services/auth.service';
import { HeartbeatService } from './services/Auth/heartbeat.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'ChitChat';
  subscription!: Subscription;

  userActiveInfo: UserActiveInfo = {
    key: '',
    value: '',
  };

  constructor(
    private heartBeatService: HeartbeatService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    if (this.authService.isAuthenticated()) {
      this.userActiveInfo.key = this.authService.decodeToken().sub;
      this.userActiveInfo.value = this.authService.decodeToken().sub;
      this.subscription = timer(0, 60000)
        .pipe(
          switchMap(() =>
            this.heartBeatService.notifyActive(this.userActiveInfo)
          )
        )
        .subscribe();
    }
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
