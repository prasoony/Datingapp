import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from 'ngx-gallery-9';
import { Member } from 'src/app/_model/member';
import { MembersService } from 'src/app/_service/members.service';

@Component({
  selector: 'app-member-details',
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.css']
})
export class MemberDetailsComponent implements OnInit {
  member: Member | undefined;
  gellaryOptions: NgxGalleryOptions[] = [];
  gallaryImages: NgxGalleryImage[] = [];


  constructor(private memberservice: MembersService, private roter: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadmember();

    this.gellaryOptions = [{
      width: '500px',
      height: '500px',
      imagePercent: 100,
      thumbnailMargin: 4,
      imageAnimation: NgxGalleryAnimation.Slide,
      preview: false


    }]
  }


  GetImage() {
    if (!this.member) return [];
    const imageUrls= [];
    for (const photo of this.member.photos) {
      imageUrls.push({
          small: photo.url,
          medium: photo.url,
          big: photo.url

        })
    }
    return imageUrls;
  }
  loadmember() {
    var user = this.roter.snapshot.paramMap.get('username');
    if (!user) return;
    this.memberservice.getMember(user).subscribe({
      next: member =>{ 
        this.member = member;
        this.gallaryImages = this.GetImage();
      }
    })
  }
}
