import { Component } from '@angular/core';
import { WeatherService } from 'src/app/weather.service';
import { Chart } from 'chart.js'

@Component({
  selector: 'forecast-root',
  templateUrl: './forecast.component.html',
  styleUrls: ['./forecast.component.css']
})
export class ForecastComponent {

  chart = [];
  filterForm;

  constructor(private _weather: WeatherService) {
  }

  renderChart(chartData) {

    let weatherDates = chartData.weatherDates;
    let temp_max = chartData.temp_max;
    let temp_max2 = chartData.temp_max2;

    let chartType = chartData.chartType;

    this.chart = new Chart('canvas', {
      type: chartType,
      data: {
        labels: weatherDates,
        datasets: [
          {
            data: temp_max,
            borderColor: 'red',
            fill: false
          },
          {
            data: temp_max2,
            borderColor: '#ffcc00',
            fill: false
          },
        ]
      },
      options: {
        legend: {
          display: false
        },
        scales: {
          xAxes: [{
            display: true
          }],
          yAxes: [{
            display: true
          }]
        }
      }
    })
  }

  getXAxis(res) {
    let alldates = res['list'].map(res => res.dt)
    let weatherDates = []
    alldates.forEach((res) => {
      let jsdate = new Date(res * 1000)
      console.log(jsdate)
      weatherDates.push(jsdate.toLocaleTimeString('en', { hour: 'numeric' }))
    })
    return weatherDates;
  }

  getChartType(filterType) {

    if (filterType == "hour") {
      return 'line'
    }
    else {
      return 'bar'
    }
  }

  filterdates(val) {

    this._weather.dailyForecastByFilter(val)
      .subscribe(res => {
        let allData = this.getGroupedData(res);

        let day1 = allData.day1
        let day2 = allData.day2
        let chartData;

        if (val.filterType == "day") {

          let highest1 = Math.max.apply(Math, res['list'].map(res => res.temp_max))
          let highest2 = Math.max.apply(Math, res['list'].map(res => res.temp_max))

          console.log(highest1)

          chartData = {
            chartType: this.getChartType(val.filterType),
            weatherDates: this.getXAxis(res),
            temp_max: highest1,
            temp_max2: highest2
          }
        }

        chartData = {
          chartType: this.getChartType(val.filterType),
          weatherDates: this.getXAxis(res),
          temp_max: day1.map(res => res.temp_max),
          temp_max2: day2.map(res => res.temp_min),
        }

        this.renderChart(chartData);
      }, error => {
          console.log()
          if (error.status == 401) {

            window.alert("Authentication failed")
          }
      })
  }

  getGroupedData(res) {
    let groups = res['list'].reduce((groups, f) => {
      let date = f.forecast_datetime.split('T')[0];

      if (!groups[date]) {
        groups[date] = [];
      }
      groups[date].push(f);
      return groups;
    }, {});


    let groupedData = {
      day1: groups[Object.keys(groups)[0]],
      day2: groups[Object.keys(groups)[1]]
    }

    return groupedData
  }

  ngOnInit() {

    this._weather.dailyForecast()
      .subscribe(res => {

        let allData = this.getGroupedData(res);

        let day1 = allData.day1
        let day2 = allData.day2

        let chartData = {
          chartType: 'line',
          weatherDates: this.getXAxis(res),
          temp_max: day1.map(res => res.temp_max),
          temp_max2: day2.map(res => res.temp_min),
        }

        this.renderChart(chartData);

      })
  }
}
