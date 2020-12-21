import { Component } from '@angular/core';
import { SpinnerService } from '../spinner.service';
import { DataService } from '../data.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  showSpinner: boolean;

  constructor(private spinnerService: SpinnerService,
    private _dataService: DataService) {
  }

  setupData() {
    this._dataService.setupData();
  }
}
