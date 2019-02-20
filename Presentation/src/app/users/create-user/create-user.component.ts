import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css']
})
export class CreateUserComponent implements OnInit {

  constructor(private userService: UserService) { }

  ngOnInit() {
    const users = this.userService.getUsers().subscribe(
      (result) => {
        alert(result);
      },
      (error) => { console.log(error); });
  }
}
