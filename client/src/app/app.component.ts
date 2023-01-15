import { Component, OnInit } from '@angular/core';
import { user } from './_model/user';
import { AccountService } from './_service/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Dating App ';
  constructor( private acccountservice: AccountService) { }

  ngOnInit(): void {
    this.SetCurrentUser();
  }
  

  SetCurrentUser() {
    const UserdString = localStorage.getItem('user')
    if (!UserdString) return;
    const user: user = JSON.parse(UserdString);
    this.acccountservice.setCurrentUser(user);
    
  }
}
