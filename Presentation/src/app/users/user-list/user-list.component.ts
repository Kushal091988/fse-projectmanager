import { User } from './../user';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  @Input() users: User[];
  @Output() userEdited = new EventEmitter<User>();
  constructor() { }

  _listFilter = '';
  _fieldName = 'id';
  get listFilter(): string {
    return this._listFilter;
  }
  set listFilter(value: string) {
    this._listFilter = value;
    this.filteredUsers = this.listFilter ? this.performFilter(this.listFilter) : this.users;
  }

  filteredUsers: User[] = [];

  performFilter(filterBy: string): User[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.users.filter((user: User) =>
      user.firstName.toLocaleLowerCase().indexOf(filterBy) !== -1);
  }

  sortByFirstName() {
    // first filter to remove other sorting
    const filteredUsers = this.performFilter(this._listFilter);
    this.filteredUsers = filteredUsers.sort((a, b) => a.firstName > b.firstName ? 1 : -1);
  }

  sortByLastName() {
    // first filter to remove other sorting
    const filteredUsers = this.performFilter(this._listFilter);
    this.filteredUsers = filteredUsers.sort((a, b) => a.lastName > b.lastName ? 1 : -1);
  }

  sortByEmployeeId() {
    // first filter to remove other sorting
    const filteredUsers = this.performFilter(this._listFilter);
    this.filteredUsers = filteredUsers.sort((a, b) => a.employeeId > b.employeeId ? 1 : -1);
  }

  ngOnInit() {
  }

  editUser(user: User) {
    this.userEdited.emit(user);
  }

}
