import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { FilterListHelper } from 'src/app/shared/filter/filter-list-helper';
import { Router } from '@angular/router';
import { TableFilterStateService } from 'src/app/shared/filter/table-filter-state.service';
import { FilterList } from 'src/app/shared/filter/filter-list';
import { Task } from '../task';
import { TaskService } from '../task.service';


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
    private taskService: TaskService) {
    this.filterListHelper = new FilterListHelper(this, this.filterStateService);
    this.filterStateService.onSort('id', -1);
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
}
