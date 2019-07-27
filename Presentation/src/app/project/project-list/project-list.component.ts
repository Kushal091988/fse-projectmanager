import { Component, OnInit, OnDestroy } from '@angular/core';
import { Project } from '../Project';
import { Subscription } from 'rxjs';
import { FilterListHelper } from 'src/app/shared/filter/filter-list-helper';
import { Router } from '@angular/router';
import { TableFilterStateService } from 'src/app/shared/filter/table-filter-state.service';
import { ProjectService } from '../project.service';
import { FilterList } from 'src/app/shared/filter/filter-list';
import { ConfirmationDialogService } from 'src/app/shared/confirm-dialog/confirmation-dialog.service';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.scss']
})
export class ProjectListComponent implements FilterList, OnInit, OnDestroy {
  projects: Project[];
  selectedProject: Project;
  cols: any[];
  private subscription: Subscription;
  totalRecords = 0;
  loading: boolean;
  priorityFilter: number;
  public filterListHelper: FilterListHelper;
  constructor(private router: Router,
    public filterStateService: TableFilterStateService,
    private projectService: ProjectService,
    private confirmationDialogService: ConfirmationDialogService,
    private messageService: MessageService) {
    this.filterListHelper = new FilterListHelper(this, this.filterStateService);
    this.filterStateService.onSort('id', -1);
  }

  ngOnInit() {
    this.cols = [
      { field: 'name', header: 'Project Name' },
      { field: 'totalTasks', header: 'Total Tasks #' },
      { field: 'priority', header: 'Priority' },
      { field: 'startDate', header: 'Start Date' },
      { field: 'endDate', header: 'End Date' },
    ];
    this.loading = true;
    this.priorityFilter = 0;
  }

  edit(project: Project): void {
    this.router.navigate([`/project/${project.id}`]);
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  refresh() {
    this.subscription = this.projectService
      .query(this.filterStateService.extract())
      .subscribe(dataResult => {
        this.projects = dataResult.data;
        this.totalRecords = dataResult.total;
        this.loading = false;
      });
  }

  loadProjectLazy($event: any) {
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

  addNewProject(): void {
    this.router.navigate([`/project/new`]);
  }

  suspendProject(project: Project): void {
    this.confirmationDialogService.confirm(`Proceed to suspend this project?`,
      () => {
        this.projectService
          .suspend(project.id)
          .subscribe(result => {
            this.messageService.add({
              severity: 'success',
              summary: project.name,
              detail: 'Suspended successfully.'
            });
            this.refresh();
          }, error => {
            this.messageService.add({
              severity: 'error',
              summary: project.name,
              detail: 'Project Could not be suspended'
            });
          });
      });
  }
}
