import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user.model';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-chatpage',
  templateUrl: './chatpage.component.html',
  styleUrls: ['./chatpage.component.css'],
})
export class ChatpageComponent implements OnInit {
  users: User[] = [];
  user: string = 'nishat';
  userClicked: boolean = false;
  constructor(private userService: UsersService) {}

  ngOnInit(): void {
    this.userService.getAllUsers().subscribe((response) => {
      this.users = response;
    });
  }

  onClick() {
    this.userClicked = true;
  }

  get userClick() {
    return this.userClicked;
  }
}
