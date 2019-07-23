import { UserService } from './../user.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { CreateUserComponent } from '../create-user/create-user.component';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  @ViewChild(CreateUserComponent) createUserComponent: CreateUserComponent;
  constructor(private userService: UserService) { }
  users: any[];
  ngOnInit() {
    this.refreshUsers();
  }

  refreshUsers() {
    this.userService.getAll().subscribe(
      (result) => {
        this.users = result;
      },
      (error) => { console.log(error); });
  }
  refreshUserForm($event: any) {
    this.createUserComponent.refreshUserForm($event);
  }
}
