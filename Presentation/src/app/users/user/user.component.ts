import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  constructor() { }
  users: any[] = [{
    FirstName: 'FirstName01',
    LastName: 'LastName01',
    Id: '01',
  },
  {
    FirstName: 'FirstName02',
    LastName: 'LastName02',
    Id: '02',
  },
  {
    FirstName: 'FirstName03',
    LastName: 'LastName03',
    Id: '03',
  },
  {
    FirstName: 'FirstName04',
    LastName: 'LastName04',
    Id: '04',
  }
    ,
  {
    FirstName: 'FirstName05',
    LastName: 'LastName05',
    Id: '05',
  }];
  ngOnInit() {

  }

}
