import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { MessageModel } from '../models/message.model';
@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  public data: MessageModel[];
  private hubConnection: signalR.HubConnection;
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5000/messages')
      .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }

  public addTransferChartDataListener = () => {
    this.hubConnection.on('MessageTransfer', (data) => {
      this.data = data;
    });
  }

  sendMessage(message: MessageModel) {
    if (message !== null) {
      this.hubConnection.invoke('SendMessage', message);
    }
  }
}
