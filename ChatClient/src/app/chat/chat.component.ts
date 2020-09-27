import { MessageService } from './../shared/services/message.service';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { HttpClient } from '@angular/common/http';
import { MessageModel } from '../shared/models/message.model';
import { AuthService } from '../shared/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css'],
  providers: []
})

export class ChatComponent implements OnInit {
  @ViewChild('scrollMe', { static: false }) private myScrollContainer: ElementRef;

  constructor(private http: HttpClient,
    private authService: AuthService,
    private messageService: MessageService) { }


  payLoad;
  hubConnection: signalR.HubConnection;
  userName = '';
  message = '';
  messages: MessageModel[] = [];
  aggregatedMessages;
  statistics;
  defaultChatView = 'chat';

  onOptionsSelectedChangeView(value: string) {
    this.defaultChatView = value;
    if (value === 'aggregated') {
      this.getAllAggregatedMessagesFromDb()
        .subscribe(data => {
          this.successAg(data);
        });
    }

    setTimeout(() => {this.scrollToBottom(); }, 500);
  }

  addHighFiveMessage(userName: string): void {
    const mes = new MessageModel(
      this.userName,
      new Date(),
      this.authService.user() + ' high-five ' + userName,
      'high-five'
    );
    this.addMessageToDb(mes).subscribe();
  }

  public sendMessage(): void {
    const mes = new MessageModel(
      this.userName,
      new Date(),
      this.message,
      'message'
    );
    this.addMessageToDb(mes).subscribe();
    this.message = '';
  }

  ngOnInit() {
    this.userName = this.authService.user();
    this.getAllMessagesFromDb()
      .subscribe(data => {
        this.success(data);
      });

    this.getAllAggregatedMessagesFromDb()
      .subscribe(data => {
        this.successAg(data);
      });

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5000/messages')
      .withAutomaticReconnect()
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));

    this.hubConnection.on('sendToAll', (messageItem: MessageModel) => {
      const text = `${messageItem.dateTime}  ${messageItem.userName}: ${messageItem.messageText}`;
      console.log(messageItem);
      this.messages.push(messageItem);
      this.statistics = this.Statistics(this.messages);
      setTimeout(() => {this.scrollToBottom(); }, 500);
    });

    setTimeout(() => {this.scrollToBottom(); }, 500);
  }

  success(data) {
    data.forEach(element => {
      const message = new MessageModel(
        element.userName,
        element.dateTime,
        element.messageText,
        element.messageType
      );

      this.messages.push(message);
      this.statistics = this.Statistics(this.messages);
    });
  }

  successAg(data) {
    console.log(data);
    this.aggregatedMessages = data;
  }

  addMessageToDb(message: MessageModel) {
    return this.messageService.addMessageToDb(message);
  }

  getAllMessagesFromDb() {
    return this.messageService.getAllMessagesFromDb();
  }

  getAllAggregatedMessagesFromDb() {
    return this.messageService.getAllAggregatedMessagesFromDb();
  }

  scrollToBottom(): void {
    try {
      this.myScrollContainer.nativeElement.scrollTop = this.myScrollContainer.nativeElement.scrollHeight;
    } catch (err) { }
  }

  public Statistics(messages: MessageModel[]) {
    let enter = 0;
    let leave = 0;
    let comments = 0;
    let highFive = 0;

    for (var i = 0; i < messages.length; ++i) {
      switch (messages[i].messageType) {
        case 'message': {
          comments++;
          break;
        }
        case 'enter': {
          enter++;
          break;
        }
        case 'leave': {
          leave++;
          break;
        }
        case 'high-five': {
          highFive++;
          break;
        }
      }
    }

    return '<p>Entered: ' + enter + ' users</p>' +
      '<p>Left: ' + leave + ' users</p>' +
      '<p>High-five: ' + highFive + ' users</p>' +
      '<p>Total messages: ' + comments + '</p>';
  }
}
