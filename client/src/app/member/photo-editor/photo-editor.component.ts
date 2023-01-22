import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs';
import { Member } from 'src/app/_model/member';
import { Photo } from 'src/app/_model/photo';
import { user } from 'src/app/_model/user';
import { AccountService } from 'src/app/_service/account.service';
import { MembersService } from 'src/app/_service/members.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
@Input() member :Member |undefined;
uploader :FileUploader |undefined;
hasBaseDropZoneOver =false;
baseUrl =environment.Apiurl;
user :user | undefined;
  constructor( private accountservice : AccountService ,private memberserice :MembersService) { 
    this.accountservice.currentUser$.pipe(take(1)).subscribe({
      next:user =>{
        if(user)  this.user =user
      }
    })
  }

  ngOnInit(): void {
    this.initializeUploader();
  }
fileOverBase(e: any)
{
this.hasBaseDropZoneOver=e;
}
setMainPhoto(photo:Photo){
this.memberserice.setMainPhoto(photo.id).subscribe(
  {
    next:() => {
      if(this.user && this.member)
      {
        this.user.photoUrl= photo.url;
        this.accountservice.setCurrentUser( this.user);
        this.member.photoUrl=photo.url;
        this.member.photos.forEach(p => {
          if(p.isMain) p.isMain=false;
          if(p.id==photo.id) p.isMain=true;
        })
      }
    }
  }
)
}
setDeletePhoto(photoId:number){
  this.memberserice.setDeletePhoto(photoId).subscribe({
    next:_ =>{
      if(this.member){
        this.member.photos=this.member.photos.filter(x =>x.id !=photoId)
      }
    }
  })

}
initializeUploader()
{
  this.uploader = new FileUploader({
    url:this.baseUrl + 'users/add-photo',
    authToken:'Bearer '+ this.user?.token,
    isHTML5:true,
    allowedFileType:['image'],
    removeAfterUpload:true,
    autoUpload:false,
    maxFileSize:10*1024*1024
  });
  this.uploader.onAfterAddingAll=(file) =>
  {
    file.withCredentials=false
  }

  this.uploader.onSuccessItem =(item ,response ,status,headers) =>{
    if(response){
      const photo = JSON.parse(response);
      this.member?.photos.push(photo);

    }
  }
}
}
