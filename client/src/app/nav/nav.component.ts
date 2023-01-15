import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';
import { user } from '../_model/user';
import { AccountService } from '../_service/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  CurrentUser$: Observable<user | null> = of(null)


  constructor(public accountservice: AccountService) { }


  ngOnInit(): void {

  }

  login() {
    this.accountservice.login(this.model).subscribe({
      next: Response => {
        console.log(Response);


      },
      error: () => console.error()

    })
  }
  LogOut() {
    this.accountservice.LogOut();

  }
}
