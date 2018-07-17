import { Component, OnInit, Input } from '@angular/core';
import { friend } from "../../shared/friend";

@Component({
  selector: 'fr-friend',
  templateUrl: './friend.component.html',
  styleUrls: ['./friend.component.css']
})
export class FriendComponent implements OnInit {

   @Input() friend: friend

  constructor() { }

  ngOnInit() {
  }

}
