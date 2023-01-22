import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../_model/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.Apiurl;
  member:Member[]=[];

 constructor( private http :HttpClient) { }

 getMembers()
 {
  if(this.member.length>0) return  of(this.member)
  return this.http.get<Member[]>(this.baseUrl + 'users').pipe(
    map(member => {
      this.member=member
      return member;

    })
  );
 }

 getMember( username: string)
 {
  const member =this.member.find(x=>x.userName===username);
  if(member) return of(member);
  return this.http.get<Member>(this.baseUrl + 'users/'+username );
  ;
 }
 setMainPhoto(photoId:number)
 {
  return  this.http.put(this.baseUrl +'Users/set-main-photo/' +photoId,{});
 }

 setDeletePhoto(photoID :number)
 {
  return  this.http.delete(this.baseUrl +'Users/delete-photo/' +photoID,{});
 }

UpdateMember(member : Member){
  return this.http.put(this.baseUrl + 'users', member ).pipe(
    map(()=>{
      const index= this.member.indexOf(member)
      this.member[index]={...this.member[index],...member}
    })
  )

}
 
}
