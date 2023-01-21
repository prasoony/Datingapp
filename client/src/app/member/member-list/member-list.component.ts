import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Member } from 'src/app/_model/member';
import { MembersService } from 'src/app/_service/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
 member$:Observable<Member[]> | undefined;

  constructor( private memberservice:MembersService) { }

  ngOnInit(): void {
   this.member$=this.memberservice.getMembers();
  }

  


}
