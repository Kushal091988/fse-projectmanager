import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { User } from 'src/app/shared/models/user';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css']
})
export class CreateUserComponent implements OnInit {
  userForm: FormGroup;
  user: User = new User();
  constructor(private userService: UserService,
    private formBuilder: FormBuilder) { }

  ngOnInit() {
    // const users = this.userService.getUsers().subscribe(
    //   (result) => {
    //     alert(result);
    //   },
    //   (error) => { console.log(error); });
    this.userForm = this.formBuilder.group({
      firstName: ['', [Validators.required, Validators.minLength(3)]],
      lastName: ['', [Validators.required, Validators.maxLength(50)]],
      employeeId: ['', [Validators.required, Validators.maxLength(50)]]
    });
  }


  onSubmit() {
    // TODO: Use EventEmitter with form value
    console.warn(this.userForm.value);
  }

  save() {
    console.log(this.userForm);
    console.log(JSON.stringify(this.userForm.value));
  }
  reset() {
    this.userForm.patchValue({
      firstName: '',
      lastName: '',
      employeeId: ''
    });
  }
}
