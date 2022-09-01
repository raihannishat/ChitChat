import { Message } from 'src/app/models/message.model';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user.model';
import { MessageService } from 'src/app/services/message.service';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-chatpage',
  templateUrl: './chatpage.component.html',
  styleUrls: ['./chatpage.component.css'],
})
export class ChatpageComponent implements OnInit {
  users: User[] = [];
  messages: Message[] = [];
  user: string = 'nishat';
  userClicked: boolean = false;
  constructor(
    private userService: UsersService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.userService.getAllUsers().subscribe((response) => {
      this.users = response;
      this.user = this.users[0].name;
    });
  }

  onClick(user: User) {
    this.userClicked = true;
    console.log(user);

    this.user = user.name;
    this.loadMessages();
  }
  loadMessages() {
    this.messageService.getMessageThread(this.user).subscribe((messages) => {
      debugger;
      this.messages = messages;
    });
  }
  get userClick() {
    return this.userClicked;
  }
}
