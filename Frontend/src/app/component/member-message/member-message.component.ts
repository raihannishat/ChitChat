import {
  ChangeDetectionStrategy,
  Component,
  Input,
  OnInit,
  ViewChild,
} from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Message } from 'src/app/models/message.model';
import { MessageService } from 'src/app/services/message.service';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-member-message',
  templateUrl: './member-message.component.html',
  styleUrls: ['./member-message.component.css'],
})
export class MemberMessageComponent implements OnInit {
  @ViewChild('messageForm') messageForm!: NgForm;
  @Input() messages!: Message[];
  username!: string | any;
  member!: string | any;
  messageContent!: string;

  constructor(
    public messageService: MessageService,
    public route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.username = localStorage.getItem('username')?.toString();
    this.route.queryParams.subscribe((params) => {
      this.member = params['membername'];
      // console.log(params['membername']);
    });
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
