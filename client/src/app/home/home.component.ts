import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  users :any;

  constructor(private http:HttpClient ) { }

  ngOnInit(): void {
    this.GetUSer();
  }
  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  GetUSer() {
    this.http.get('https://localhost:7005/api/users').subscribe({

      next: Response => this.users = Response,
      error: error => console.log(error),
      complete: () => console.log('reqpuest completed')


    }
    )
  }

  cancleRegisterMode(event :boolean){
this.registerMode=event;
  }
}
