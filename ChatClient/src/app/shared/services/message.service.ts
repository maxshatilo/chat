import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { MessageModel } from '../models/message.model';


const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };

@Injectable({
  providedIn: 'root'
})

export class MessageService {

  constructor(private http: HttpClient) { }

  webApiUrl = environment.baseUrl;

  addMessageToDb(message: MessageModel) {
    return this.http.post(this.webApiUrl + '/api/messages', message, httpOptions)
      .pipe(map(data => data), catchError(this.handleError));
  }

  getAllMessagesFromDb() {
    return this.http.get<MessageModel>(this.webApiUrl + '/api/messages', httpOptions)
      .pipe(map(data => data), catchError(this.handleError));
  }

  getAllAggregatedMessagesFromDb() {
    return this.http.get(this.webApiUrl + '/api/messages/aggregated', httpOptions)
      .pipe(map(data => data), catchError(this.handleError));
  }

  private handleError(errorResponse: HttpErrorResponse) {
    if (errorResponse.error instanceof ErrorEvent) {
      console.error('Client side error: ', errorResponse.error.message);
    } else {
      console.error('Server side error: ', errorResponse);
    }

    return throwError(`Error: ${errorResponse}`);
  }
}
