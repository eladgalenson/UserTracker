import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { FriendsComponent } from './friends/friends.component';
import { FriendComponent } from './friends/friend.component';
import { DataService } from '../shared/data.service';



@NgModule({
  declarations: [
      AppComponent,
      FriendsComponent,
      FriendComponent
  ],
  imports: [
      BrowserModule,
      HttpClientModule
  ],
    providers: [DataService],
  bootstrap: [AppComponent]
})
export class AppModule { }
