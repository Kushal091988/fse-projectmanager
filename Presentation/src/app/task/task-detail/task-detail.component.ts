import { ParentTaskService } from './../parent-task.service';
import { ProjectService } from './../../project/project.service';
import { TaskService } from './../task.service';
import { Task } from './../task';
import { MessageService } from 'primeng/api';
import { common } from 'src/app/core/common';
import {
  ConfirmationDialogService
} from 'src/app/shared/confirm-dialog/confirmation-dialog.service';

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { UserService } from 'src/app/users/user.service';
import { User } from 'src/app/users/user';
import { Project } from 'src/app/project/Project';


@Component({
  selector: 'app-task-detail',
  templateUrl: './task-detail.component.html',
  styleUrls: ['./task-detail.component.scss']
})
export class TaskDetailComponent implements OnInit {
  startDate: Date;
  endDate: Date;
  projectOptions: Project[];
  ownerOptions: User[];
  parentTaskOptions: Task[];
  ready = true;
  isParentTask = false;
  public currentTask: Task;
  public selectedProject: Project;
  public selectedOwner: User;
  public selectedParentTask: Task;

  get readonly(): boolean {
    return false;
  }

  constructor(private router: ActivatedRoute,
    private route: Router,
    public userService: UserService,
    private messageService: MessageService,
    private confirmationDialogService: ConfirmationDialogService,
    private taskService: TaskService,
    private projectService: ProjectService,
    private parentTaskService: ParentTaskService) { }

  ngOnInit() {
    this.instantiateTask(undefined);
    this.load();
  }

  loadOwners() {
    this.userService.getAll()
      .subscribe(users => {
        this.ownerOptions = users;
        this.selectedOwner = this.ownerOptions.find(m => m.id === this.currentTask.ownerId);
      });
  }

  loadProjects() {
    this.projectService.getAll()
      .subscribe(projects => {
        this.projectOptions = projects;
        this.selectedProject = this.projectOptions.find(m => m.id === this.currentTask.projectId);
      });
  }

  loadParentTasks() {
    this.parentTaskService.getAll()
      .subscribe(tasks => {
        this.parentTaskOptions = tasks;
        this.selectedParentTask = this.parentTaskOptions.find(m => m.id === this.currentTask.parentTaskId);
      });
  }

  load() {
    const id = this.router.snapshot.paramMap.get('id');
    if (!common.isNil(id)) {
      this.taskService.get(id)
        .subscribe(task => {
          this.instantiateTask(task);
          this.ready = true;
          this.startDate = common.YYYYMMDDToDate(this.currentTask.startDate);
          this.endDate = common.YYYYMMDDToDate(this.currentTask.endDate);
          this.loadProjects();
          this.loadOwners();
          this.loadParentTasks();
        });
    } else {
      this.loadProjects();
      this.loadOwners();
      this.loadParentTasks();
      this.ready = true;
    }
  }
  save() {
    const dto = this.extractDto();
    const action = common.isNil(dto.id) ? 'create' : 'update';
    this.confirmationDialogService.confirm(`Proceed to ${action} this task?`,
      () => {

        if (this.isParentTask) {
          this.saveParentTask(dto);
        } else {
          this.saveTask(dto);
        }
      });
  }

  saveTask(dto: Task) {
    this.taskService
      .update(dto)
      .subscribe(result => {
        // clear form
        this.instantiateTask(null);
        this.messageService.add({
          severity: 'success',
          summary: this.currentTask.name,
          detail: 'Saved successfully.'
        });
        this.back();
      });
  }

  saveParentTask(dto: Task) {
    this.parentTaskService
      .create(dto)
      .subscribe(result => {
        // clear form
        this.instantiateTask(null);
        this.messageService.add({
          severity: 'success',
          summary: this.currentTask.name,
          detail: 'Saved successfully.'
        });
        this.back();
      });
  }

  back() {
    this.route.navigate(['task/list']);
  }

  instantiateTask(task: Task) {
    if (common.isNil(task)) {
      this.currentTask = {
        id: 0,
        name: '',
        startDate: '',
        endDate: '',
        priority: '0',
        ownerName: '',
        ownerId: 0,
        projectName: '',
        projectId: 0,
        parentTaskName: '',
        parentTaskId: 0,
        statusId: 1
      };
    } else {
      this.currentTask = common.cloneDeep(task);
    }
  }

  extractDto(): Task {
    return {
      id: this.currentTask.id,
      name: this.currentTask.name,
      startDate: common.isNil(this.startDate) ? '' : common.dateToYYYYMMDD(this.startDate),
      endDate: common.isNil(this.endDate) ? '' : common.dateToYYYYMMDD(this.endDate),
      priority: this.currentTask.priority,
      ownerId: common.isNil(this.selectedOwner) ? 0 : this.selectedOwner.id,
      projectId: common.isNil(this.selectedProject) ? 0 : this.selectedProject.id,
      parentTaskId: common.isNil(this.selectedParentTask) ? 0 : this.selectedParentTask.id,
      ownerName: undefined,
      projectName: undefined,
      parentTaskName: undefined,
      statusId: this.currentTask.statusId
    };
  }

  onOwnerChange($event: any) {
    this.selectedOwner = $event.value;
  }

  onProjectChange($event: any) {
    this.selectedProject = $event.value;
  }

  onParentTaskChange($event: any) {
    this.selectedParentTask = $event.value;
  }
}

