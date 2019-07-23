import { User } from './../user';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { UserService } from '../user.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ConfirmationDialogService } from 'src/app/shared/confirm-dialog/confirmation-dialog.service';



@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css']
})
export class CreateUserComponent implements OnInit {
  userForm: FormGroup;

  @Output() addedNewUser = new EventEmitter<boolean>();

  constructor(private userService: UserService,
    private confirmationDialogService: ConfirmationDialogService,
    private messageService: MessageService,
    private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.userForm = this.formBuilder.group({
      firstName: ['', [Validators.required, Validators.minLength(3)]],
      lastName: ['', [Validators.required, Validators.maxLength(50)]],
      employeeId: ['', [Validators.required, Validators.maxLength(50)]]
    });

    this.userForm.reset();
  }

  createNewUser() {
    const user: User = {
      id: 0,
      firstName: this.userForm.value.firstName,
      lastName: this.userForm.value.lastName,
      employeeId: this.userForm.value.employeeId
    };

    this.confirmationDialogService.confirm('',
      () => {
        this.userService
          .create(user)
          .subscribe(result => {
            // clear form
            this.reset();
            this.messageService.add({
              severity: 'success',
              summary: result.firstName,
              detail: 'Saved successfully.'
            });

            // emit result
            this.addedNewUser.emit(true);
          });
      });


    // this.userService.create(user).subscribe(
    //   (result) => {
    //     this.reset();
    //   },
    //   (error) => { console.log(error); });
  }

  reset() {
    this.userForm.reset();
  }

  refreshUserForm(user: User) {
    this.userForm.patchValue({
      firstName: user.firstName,
      lastName: user.lastName,
      employeeId: user.employeeId
    });
  }
}
