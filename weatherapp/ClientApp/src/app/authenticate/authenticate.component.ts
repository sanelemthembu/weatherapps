import { Component } from '@angular/core';
import { UserService } from '../user.service';

@Component({
  selector: 'app-authenticate-component',
  templateUrl: './authenticate.component.html'
})
export class AuthenticateComponent {

  constructor(private _userService: UserService) {
  }

  auth(val) {
    this._userService.authenticate(val).subscribe(t => {
      console.log(t['token'])
      document.getElementById("token").innerHTML = t['token']
    });
  }

}
