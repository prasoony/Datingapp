import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MemberListComponent } from './member/member-list/member-list.component';
import { MemberDetailsComponent } from './member/member-details/member-details.component';
import { ListsComponent } from './lists/lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { SharedModule } from './_modules/shared.module';
import { TestErrorComponent } from './error/test-error/test-error.component';
import { ErrorInterceptor } from './_interceptor/error.interceptor';
import { JwtInterceptor } from './_interceptor/jwt.interceptor';
import { MemberCardComponent } from './member/member-card/member-card.component';
import { MemberEditComponent } from './member/member-edit/member-edit.component';
import { LoadingInterceptor } from './_interceptor/loading.interceptor';
import { PhotoEditorComponent } from './member/photo-editor/photo-editor.component';



@NgModule({
  declarations: [
    
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    MemberListComponent,
    MemberDetailsComponent,
    ListsComponent,
    MessagesComponent,
    TestErrorComponent,
    MemberCardComponent,
    MemberEditComponent,
    PhotoEditorComponent,
   

  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    SharedModule
    
  ],
  providers: [
    {provide:HTTP_INTERCEPTORS,useClass:ErrorInterceptor ,multi:true},
    {provide:HTTP_INTERCEPTORS,useClass:JwtInterceptor ,multi:true},
    {provide:HTTP_INTERCEPTORS,useClass:LoadingInterceptor ,multi:true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
