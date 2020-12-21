import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';

@Injectable()
export class DataService {

  constructor(private _http: HttpClient) { }
  
  setupData() {
    console.log('setupData')
    this._http.get("https://localhost:44331/data")
      .map(t => t).subscribe(r => {
        console.log(r)
        window.alert("data generated")
      });
  }  
}
