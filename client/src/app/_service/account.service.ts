import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { user } from '../_model/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.Apiurl;
  private CurrentUserSource=  new BehaviorSubject<user | null>(null)
  currentUser$=this.CurrentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post<user>(this.baseUrl + 'account/login', model).pipe(
      map((Response: user) => {
        const user = Response;
        if (user) {
        
        localStorage.setItem('user', JSON.stringify(user))
        this.CurrentUserSource.next(user)
        }
      })
    )
  }
  register(model:any)
  {
  return this.http.post<user>(this.baseUrl + 'account/register',model).pipe(
    map(user =>{
      if(user){
        localStorage.setItem('user',JSON.stringify(user));
        this.CurrentUserSource.next(user)

      }
    })
  )
  }
  setCurrentUser(user:user){
    this.CurrentUserSource.next(user);
  }
  LogOut()
  {
    localStorage.removeItem('user');
    this.CurrentUserSource.next(null);
  }
}
