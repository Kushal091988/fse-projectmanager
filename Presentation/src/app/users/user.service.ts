import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppHttpService } from '../shared/http-service/app-http.service';
import { environment } from 'src/environments/environment';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpService: AppHttpService) { }

  getUsers(): Observable<any> {
    return this.httpService.get<any>({ url: environment.apiUrl + '/api/users/getUsers' });
  }
}
