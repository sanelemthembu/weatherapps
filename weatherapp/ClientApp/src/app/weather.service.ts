import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import 'rxjs/add/operator/map';

@Injectable()
export class WeatherService {

  constructor(private _http: HttpClient) { }

  dailyForecast() {

    return this._http.get("https://localhost:44331/weather")
      .map(result => result);
  }

  dailyForecastByFilter(val) {

    let headr = new HttpHeaders();
    this._http.post("https://localhost:44331/api/token", { Username: "test", Password: "P@ssword123" })
      .map(t => t)
      .subscribe(t => {
        console.log(t['token'])
        headr = headr.set('Authorization', 'Bearer ' + t['token']);
      });       

    return this._http.get("https://localhost:44331/weather/" + val.filterdate,
      {
        headers: headr
      })
      .map(result => result);
  }
}
