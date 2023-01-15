import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_service/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() CancelRegister = new EventEmitter();
  model: any = {}
  constructor(private accountservice: AccountService) { }

  ngOnInit(): void {
  }
  register() {
    this.accountservice.register(this.model).subscribe({
      next: () => {
        this.Cancel();
      },
      error: error => console.log(error)

    })

  }
  Cancel() {
    this.CancelRegister.emit(false);
  }
}
