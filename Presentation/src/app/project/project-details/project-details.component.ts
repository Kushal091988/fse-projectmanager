import { ProjectService } from './../project.service';
import { MessageService } from 'primeng/api';
import { common } from 'src/app/core/common';
import {
  ConfirmationDialogService
} from 'src/app/shared/confirm-dialog/confirmation-dialog.service';

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Project } from '../Project';
import { User } from 'src/app/users/user';
import { UserService } from 'src/app/users/user.service';

@Component({
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styleUrls: ['./project-details.component.scss']
})
export class ProjectDetailsComponent implements OnInit {
  startDate: Date;
  endDate: Date;
  managerOptions: User[];
  ready = true;
  public currentProject: Project;
  public selectedManager: User;
  isSetDate: boolean;
  get readonly(): boolean {
    return false;
  }

  constructor(private router: ActivatedRoute,
    private route: Router,
    public userService: UserService,
    private messageService: MessageService,
    private confirmationDialogService: ConfirmationDialogService,
    private projectService: ProjectService) { }

  ngOnInit() {
    this.instantiateProject(undefined);
    this.load();
  }
  loadManagers() {
    this.userService.getAll()
      .subscribe(users => {
        this.managerOptions = users;
        this.selectedManager = this.managerOptions.find(m => m.id === this.currentProject.managerId);
      });
  }

  load() {
    const id = this.router.snapshot.paramMap.get('id');
    if (!common.isNil(id)) {
      this.projectService.get(id)
        .subscribe(project => {
          this.instantiateProject(project);
          this.ready = true;
          this.startDate = common.YYYYMMDDToDate(this.currentProject.startDate);
          this.endDate = common.YYYYMMDDToDate(this.currentProject.endDate);
          this.loadManagers();
        });
    } else {
      this.ready = true;
      this.loadManagers();
    }
  }
  save() {
    const dto = this.extractDto();
    const action = common.isNil(dto.id) ? 'create' : 'update';
    this.confirmationDialogService.confirm(`Proceed to ${action} this user?`,
      () => {
        this.projectService
          .update(dto)
          .subscribe(result => {
            // clear form
            this.instantiateProject(null);
            this.messageService.add({
              severity: 'success',
              summary: this.currentProject.name,
              detail: 'Saved successfully.'
            });
            this.back();
          });
      });
  }
  back() {
    this.route.navigate(['project/list']);
  }



  instantiateProject(project: Project) {

    if (common.isNil(project)) {
      this.currentProject = {
        id: 0,
        name: '',
        startDate: '',
        endDate: '',
        priority: '0',
        managerDisplayName: '',
        managerId: 0
      };
    } else {
      this.currentProject = common.cloneDeep(project);
    }
  }

  public extractDto(): Project {
    return {
      id: this.currentProject.id,
      name: this.currentProject.name,
      startDate: common.isNil(this.startDate) ? '' : common.dateToYYYYMMDD(this.startDate),
      endDate: common.isNil(this.endDate) ? '' : common.dateToYYYYMMDD(this.endDate),
      priority: this.currentProject.priority,
      managerDisplayName: this.selectedManager.displayName,
      managerId: this.selectedManager.id
    };
  }
  onManagerChange($event: any) {
    this.selectedManager = $event.value;
  }
}
