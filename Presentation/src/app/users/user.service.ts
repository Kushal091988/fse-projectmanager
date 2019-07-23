import { User } from './user';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppHttpService } from '../shared/http-service/app-http.service';
import { environment } from 'src/environments/environment';
import { catchError } from 'rxjs/operators';
import { appSettings } from '../app.settings';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpService: AppHttpService) { }
  private readonly url: string = appSettings.api.user.path;
  getAll(): Observable<User> {
    return this.httpService.get<User>({ url: this.url + '/getUsers' });
  }

  create(user: User): Observable<User> {
    return this.httpService.post<User>({ url: `${this.url}/update` }, user);
  }

  update(user: User): Observable<User> {
    return this.httpService.post<User>({ url: `${this.url}/update` }, user);
  }

  get(id: number): Observable<User> {
    return this.httpService.get<User>({ url: `${this.url}/${id}` });
  }

  delete(id: number): Observable<User> {
    return this.httpService.delete<User>({ url: `${this.url}/delete/${id}` });
  }
}
