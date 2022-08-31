import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Message } from 'src/app/models/message.model';
import { MessageService } from 'src/app/services/message.service';

@Component({
  selector: 'app-chatbox',
  templateUrl: './chatbox.component.html',
  styleUrls: ['./chatbox.component.css'],
})
export class ChatboxComponent implements OnInit {
  @ViewChild('messageForm') messageForm!: NgForm;
  @Input() messages!: Message[];
  username!: string | any;
  @Input() member!: string | any;
  messageContent!: string;

  constructor(
    public messageService: MessageService,
    public route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.username = localStorage.getItem('username')?.toString();
    this.messageService.createHubConnection(this.username, this.member);
  }

  loadMessages() {
    this.messageService.getMessageThread(this.member).subscribe((messages) => {
      this.messages = messages;
    });
  }

  sendMessage() {
    console.log(this.username);
    console.log(this.messageContent);
    this.messageService
      .sendMessage(this.username, this.member, this.messageContent)
      .then(() => {
        this.messageForm.reset();
      });
  }

  ngOnDestroy(): void {
    this.messageService.stopHubConnection();
  }
}
