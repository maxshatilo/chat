import { MessageModel } from './../../shared/models/message.model';
import { MessageService } from './../../shared/services/message.service';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from 'src/app/shared/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-chat',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  payLoad;
  submitted = false;
  returnUrl: string;
  error: {};
  loginError: string;
  errorText = false;
  message = '';

  constructor(private authService: AuthService,
    private router: Router,
    private messageService: MessageService) { }

  ngOnInit() {
    this.authService.logout();
  }

  onSubmit(form: NgForm) {
    this.authService.login(form.value.userName, form.value.password).subscribe(
      (res: any) => {
        localStorage.setItem('token', res.token);
        this.router.navigateByUrl('/chat');
        this.messageService.addMessageToDb(new MessageModel(
          this.authService.user(),
          new Date(),
          'enters the room',
          'enter'
        )).subscribe();
      },
      err => {
        if (err.status == 401)
          this.errorText = true;
        else
          console.log(err);
      }
    );
  }
}
