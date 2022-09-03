import { Message } from 'src/app/models/message.model';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user.model';
import { MessageService } from 'src/app/services/message.service';
import { UsersService } from 'src/app/services/users.service';
import { ActiveUser } from 'src/app/models/activeUser';
import { ActiveUsersService } from 'src/app/services/active-users.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css'],
})
export class ChatComponent implements OnInit {
  activeUsers: string[] = [];
  sender: string = localStorage.getItem('username')?.toString()!;
  messages: Message[] = [];
  member: string = 'nishat';
  userClicked: boolean = false;
  constructor(
    private activeUserService: ActiveUsersService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.activeUserService.getActiveUsersList().subscribe((response) => {
      this.activeUsers = response;
      console.log(this.activeUsers);
    });
  }

  onClick(user: string) {
    this.userClicked = true;
    this.member = user;
    console.log(this.member);
    this.messageService.stopHubConnection();
    this.messageService.createHubConnection(this.sender, this.member);
  }

  get userClick() {
    return this.userClicked;
  }
}
