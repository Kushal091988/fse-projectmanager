import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { FilterListHelper } from 'src/app/shared/filter/filter-list-helper';
import { Router } from '@angular/router';
import { TableFilterStateService } from 'src/app/shared/filter/table-filter-state.service';
import { FilterList } from 'src/app/shared/filter/filter-list';
import { Task } from '../task';
import { TaskService } from '../task.service';
import { ConfirmationDialogService } from 'src/app/shared/confirm-dialog/confirmation-dialog.service';
import { MessageService } from 'primeng/api';
import { FilterOperatorType } from 'src/app/shared/filter/models/filter-enums';


@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.scss']
})
export class TaskListComponent implements FilterList, OnInit, OnDestroy {
  tasks: Task[];
  selectedProject: Task;
  cols: any[];
  private subscription: Subscription;
  totalRecords = 0;
  loading: boolean;
  priorityFilter: number;
  public filterListHelper: FilterListHelper;
  constructor(private router: Router,
    public filterStateService: TableFilterStateService,
    private taskService: TaskService,
    private confirmationDialogService: ConfirmationDialogService,
    private messageService: MessageService) {
    this.filterListHelper = new FilterListHelper(this, this.filterStateService);
    this.filterStateService.onSort('id', -1);
    this.filterStateService.onFilter('status', '3', FilterOperatorType.notEqualTo);
  }

  ngOnInit() {
    this.cols = [
      { field: 'name', header: 'Task' },
      { field: 'parentTaskName', header: 'Parent Task' },
      { field: 'priority', header: 'Priority#' },
      { field: 'projectName', header: 'Project' },
      { field: 'ownerName', header: 'Owner' },
      { field: 'startDate', header: 'Start Date' },
      { field: 'endDate', header: 'End Date' },
    ];
    this.loading = true;
    this.priorityFilter = 0;
  }

  edit(task: Task): void {
    this.router.navigate([`/task/${task.id}`]);
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  refresh() {
    this.subscription = this.taskService
      .query(this.filterStateService.extract())
      .subscribe(dataResult => {
        this.tasks = dataResult.data;
        this.totalRecords = dataResult.total;
        this.loading = false;
      });
  }

  loadTaskLazy($event: any) {
    this.loading = true;
    this.filterStateService.onPaginate($event.first, $event.rows);
    let sortField = $event.sortField;
    let sortOrder = $event.sortOrder;
    if (!sortField) {
      sortField = 'id';
      sortOrder = -1;
    }
    this.filterStateService.onSort(sortField, sortOrder);
    this.refresh();
  }

  addNewTask(): void {
    this.router.navigate([`/task/new`]);
  }

  completeTask(task: Task): void {
    this.confirmationDialogService.confirm(`Proceed to complete this task?`,
      () => {
        this.taskService
          .complete(task.id)
          .subscribe(result => {
            this.messageService.add({
              severity: 'success',
              summary: task.name,
              detail: 'Completed successfully.'
            });
            this.refresh();
          }, error => {
            this.messageService.add({
              severity: 'error',
              summary: task.name,
              detail: 'Task Could not be completed'
            });
          });
      });
  }
}
