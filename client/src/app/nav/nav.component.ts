import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, of } from 'rxjs';
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


  constructor(public accountservice: AccountService , private router: Router ,
    private toastr: ToastrService) { }


  ngOnInit(): void {

  }

  login() {
    this.accountservice.login(this.model).subscribe({
      next:_ =>this.router.navigateByUrl('/member'),
      error:error=>this.toastr.error(error)
    })
  }
  LogOut() {
    this.accountservice.LogOut();
    this.router.navigateByUrl('/');

  }
}
