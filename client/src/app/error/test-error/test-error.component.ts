import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.css']
})
export class TestErrorComponent implements OnInit {
  baseurl = environment.Apiurl;

  constructor( private http:HttpClient) { }

  ngOnInit(): void {
  }
  
  get400Error()
  {
    this.http.get(this.baseurl + 'buggy/bad-request').subscribe({
      next:response => console.log(response),
      error:error=>console.log(error)
      
    })
  }
  get500Error()
  {
    this.http.get(this.baseurl + 'buggy/Server-error').subscribe({
      next:response => console.log(response),
      error:error=>console.log(error)
      
    })
  }
  get401Error()
  {
    this.http.get(this.baseurl + 'buggy/auth').subscribe({
      next:response => console.log(response),
      error:error=>console.log(error)
      
    })
  }
  get400ValidationError()
  {
    this.http.post(this.baseurl + 'buggy/Regsiter',{}).subscribe({
      next:response => console.log(response),
      error:error=>console.log(error)
      
    })
  }
  get404Error()
  {
    this.http.get(this.baseurl + 'buggy/not-found').subscribe({
      next:response => console.log(response),
      error:error=>console.log(error)
      
    })
  }
}

