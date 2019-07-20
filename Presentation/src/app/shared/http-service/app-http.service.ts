import { Injectable } from '@angular/core';
import { of, throwError } from 'rxjs';
import { catchError, map, retry, tap } from 'rxjs/operators';

import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { AppHttpConfig } from './app-http-config';

@Injectable({
  providedIn: 'root'
})
export class AppHttpService {

  constructor(private http: HttpClient) { }

  private createHttpOptions() {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return {
      headers: headers
    };
  }

  get<T>(config: AppHttpConfig) {
    const httpOptions = this.createHttpOptions();
    return this.http.get<T>(config.url, httpOptions)
      .pipe(
        // retry(3), // retry a failed request up to 3 times
        catchError(this.handleError)
      );
  }

  post<T>(config: AppHttpConfig, data: any) {
    return this.http.post<T>(config.url, data, this.createHttpOptions())
      .pipe(
        tap((r) => {
          console.log(r);
        }),
        catchError(this.handleError)
      );
  }

  update<T>(config: AppHttpConfig, data: any) {
    return this.http.put<T>(config.url, data, this.createHttpOptions())
      .pipe(
        tap((r) => {
          console.log(r);
        }),
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // return an observable with a user-facing error message
    return throwError(
      'Something bad happened; please try again later.');
  }
}
