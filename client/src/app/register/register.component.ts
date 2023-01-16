import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_service/account.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() CancelRegister = new EventEmitter();
  model: any = {}
  constructor(private accountservice: AccountService ,private toastr :ToastrService) { }

  ngOnInit(): void {
  }
  register() {
    this.accountservice.register(this.model).subscribe({
      next: () => {
        this.Cancel();
      },
      error:error=>this.toastr.error(error)    

    })

  }
  Cancel() {
    this.CancelRegister.emit(false);
  }
}
