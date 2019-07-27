import { MessageService } from 'primeng/api';
import { common } from 'src/app/core/common';
import {
  ConfirmationDialogService
} from 'src/app/shared/confirm-dialog/confirmation-dialog.service';

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../user.service';
import { User } from '../user';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.scss']
})
export class UserDetailsComponent implements OnInit {

  ready = true;
  /** Current editing warehouse instance */
  public currentUser: User;

  get readonly(): boolean {
    return false;
  }

  constructor(private router: ActivatedRoute,
    private route: Router,
    public userService: UserService,
    private messageService: MessageService,
    private confirmationDialogService: ConfirmationDialogService) {
  }

  ngOnInit() {
    this.instantiateUser(undefined);
    this.load();
  }

  load() {
    const id = this.router.snapshot.paramMap.get('id');
    if (!common.isNil(id)) {
      this.userService.get(id)
        .subscribe(user => {
          this.instantiateUser(user);
          this.ready = true;
        });
    } else {
      this.ready = true;
    }
  }
  save() {
    const dto = this.extractDto();
    const action = common.isNil(dto.id) ? 'create' : 'update';
    this.confirmationDialogService.confirm(`Proceed to ${action} this user?`,
      () => {
        this.userService
          .update(dto)
          .subscribe(result => {
            // clear form
            this.instantiateUser(null);
            this.messageService.add({
              severity: 'success',
              summary: this.currentUser.firstName,
              detail: 'Saved successfully.'
            });
            this.back();
          });
      });
  }
  back() {
    this.route.navigate(['user/list']);
  }



  instantiateUser(user: User) {

    if (common.isNil(user)) {
      this.currentUser = {
        id: 0,
        firstName: '',
        lastName: '',
        employeeId: ''
      };
    } else {
      this.currentUser = common.cloneDeep(user);
    }
  }

  public extractDto(): User {
    return {
      id: this.currentUser.id,
      firstName: this.currentUser.firstName,
      lastName: this.currentUser.lastName,
      employeeId: this.currentUser.employeeId
    };
  }

}
