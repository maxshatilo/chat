import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { User } from '../models/user.model';

const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  serverUrl = environment.baseUrl + '/api/account/';
  errorData: {};

  constructor(private http: HttpClient) { }

  redirectUrl: string;

  login(username: string, password: string) {
    return this.http.post(this.serverUrl + 'login/', { username, password });
  }

  getAllUsers() {
    return this.http.get<User>(this.serverUrl + 'users/', httpOptions)
      .pipe(map(data => data), catchError(this.handleError));
  }

  getUserByUserName(userName: string) {
    return this.http.get(this.serverUrl + 'user/' + userName);
  }

  addUser(user: User) {
    return this.http.post<User>(this.serverUrl + 'register/', user, httpOptions)
      .pipe(map(data => data), catchError(this.handleError));
  }

  isLoggedIn() {
    if (localStorage.getItem('token')) {
      return true;
    }
    return false;
  }

  getAuthorizationToken() {
    if (localStorage.getItem('token')) {
      const currentUser = JSON.parse(localStorage.getItem('token'));
      return currentUser.token;
    }
    return null;
  }

  logout() {
    localStorage.removeItem('token');
  }

  user(): any {
    let userName;
    if (localStorage.getItem('token') != null) {
      userName = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
    }
    return userName != null ? userName.unique_name.toUpperCase() : 'Not Set';
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {

      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {

      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(`Backend returned code ${error.status}, ` + `body was: ${error.error}`);
    }

    // return an observable with a user-facing error message
    this.errorData = {
      errorTitle: 'Oops! Request for document failed',
      errorDesc: 'Something bad happened. Please try again later.'
    };
    return throwError(this.errorData);
  }
}
