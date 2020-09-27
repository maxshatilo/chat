import { Component, AfterViewChecked } from '@angular/core';
import { MessageModel } from '../shared/models/message.model';
import { AuthService } from '../shared/services/auth.service';
import { MessageService } from '../shared/services/message.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements AfterViewChecked {

  constructor(public authService: AuthService,
    private messageService: MessageService) { }

  user;

  ngAfterViewChecked() {
    setTimeout(() => {
      this.user = this.authService.user();
    }, 1000);
  }

  sendLogOutMessage() {
    this.messageService.addMessageToDb(new MessageModel(
      this.authService.user(),
      new Date(),
      'leaves the room',
      'leave'
    )).subscribe();
  }
}
