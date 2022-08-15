import { Component, OnInit } from '@angular/core';
import { ActiveUser } from 'src/app/models/activeUser';
import { ActiveUsersService } from 'src/app/services/active-users.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css'],
})
export class ChatComponent implements OnInit {
  activeUsers: ActiveUser[] = [];
  constructor(private activeUserServie: ActiveUsersService) {}

  ngOnInit(): void {
    this.activeUserServie.activeUsersList().subscribe((response) => {
      this.activeUsers = response;
    });
  }
}
