import { Component, OnInit } from '@angular/core';
import { DataService } from '../../shared/data.service';

import { friend } from "../../shared/friend";

@Component({
  selector: 'app-friends',
  templateUrl: './friends.component.html',
  styleUrls: ['./friends.component.css']
})
export class FriendsComponent implements OnInit {

    public friends: Array<friend>;

    constructor(private dataService: DataService) { }

    ngOnInit() {
        this.dataService.getFriends().subscribe(
            data => { this.friends = data},
            err => console.error(err),
            () => console.log('done loading foods'));;

  }

}
