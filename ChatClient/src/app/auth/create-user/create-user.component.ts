import { RegisterUser } from './../../shared/models/registerUser.model';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../shared/services/auth.service';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css']
})
export class CreateUserComponent implements OnInit {
  @ViewChild('f', { static: false }) addUserForm: NgForm;
  user;
  userExist = false;
  errorText: string;
  userN;
  userItem;
  data;
  password;
  confirmPassword;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {}

  success(data) {
    console.log(data);
  }

  failed(err) {
    console.log(err);
  }

  matchingConfirmPasswords() {
    if (this.password !== this.confirmPassword) {
      return false;
    } else {
      return true;
    }
  }

  userNameFocusOutFunction(userName: string) {
    this.authService.getUserByUserName(userName)
      .subscribe(result => {
        if (userName === this.userN) {
          this.userExist = false;
        } else {
          this.userExist = true;
        }
      },
        error => {
          this.userExist = false;
        });
  }

  onAddUser(form: NgForm): void {
    this.getFormData(form);
    this.authService.addUser(this.user)
      .subscribe(
        (data: any) => {
          console.log('Success: ', this.data = data);
          localStorage.setItem('token', data.token);
          this.router.navigate(['/chat'], { queryParams: { item: this.user.UserName } });
        }
      );
  }

  getFormData(form: NgForm) {
    this.user = new RegisterUser(
      form.value.userName,
      form.value.password == null ? '' : form.value.password,
      form.value.confirmPassword == null ? '' : form.value.confirmPassword
    );
  }
}
