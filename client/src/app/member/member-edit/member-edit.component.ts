import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { Member } from 'src/app/_model/member';
import { user } from 'src/app/_model/user';
import { AccountService } from 'src/app/_service/account.service';
import { MembersService } from 'src/app/_service/members.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm|undefined;
  @HostListener('window:beforeunload',['$event']) unloadNotification($event:any){
    if(this.editForm?.dirty)
    $event.returnValue= true;
  }
  member :Member|undefined;
  user : user|null=null;

  constructor( private accountservice:AccountService , private memberservice:MembersService ,private toaster:ToastrService) { 
    this.accountservice.currentUser$.pipe(take(1)).subscribe({
      next:user=> this.user=user
    })
  }

  ngOnInit(): void {
    this.loadmember();
  }


  loadmember()
  {
    if(!this.user) return ;
    this.memberservice.getMember(this.user.username).subscribe({
      next: member=> this.member =member
    })
  }
  updateMember()
  {
    this.memberservice.UpdateMember(this.editForm?.value).subscribe({
      next: _ =>{
        this.toaster.success("Profile Update Successfully");
        this.editForm?.reset(this.member);
      }
    })
    
  }
}
