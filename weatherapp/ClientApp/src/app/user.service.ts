import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';

@Injectable()
export class UserService {

  constructor(private _http: HttpClient) { }

  public generatedToken: any;

  addUser(val) {

    this._http.post("https://localhost:44331/register", { Username: val.username, name: val.name })
      .map(t => t);
  }
  authenticate(val) {

    return this._http.post("https://localhost:44331/authenticate", { Username: val.Username, Password: val.Password })
      .map(t => t);
  }
}
