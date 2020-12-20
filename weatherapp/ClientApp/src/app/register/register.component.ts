import { Component } from '@angular/core';
import { UserService } from '../user.service';

@Component({
  selector: 'app-register-component',
  templateUrl: './register.component.html'
})
export class RegisterComponent {

  constructor(private _userService: UserService) {
  }

  addUser(val) {
    console.log(val)
    this._userService.addUser(val)
  }

}
