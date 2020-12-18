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
    return this._http.get("https://localhost:44331/weather/"+val.filterdate)
      .map(result => result);
  }
}
