import { Component, OnInit } from '@angular/core';
import { tick } from '@angular/core/testing';
import { User } from 'src/app/models/user.model';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-chitchat',
  templateUrl: './chitchat.component.html',
  styleUrls: ['./chitchat.component.css'],
})
export class ChitchatComponent implements OnInit {
  users: User[] = [];
  constructor(private userService: UsersService) {}

  ngOnInit(): void {
    this.userService.getAllUsers().subscribe((response) => {
      this.users = response;
    });
  }
}
