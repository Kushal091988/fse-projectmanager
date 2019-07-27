import { Component, OnInit, OnDestroy } from '@angular/core';
import { Project } from '../Project';
import { Subscription } from 'rxjs';
import { FilterListHelper } from 'src/app/shared/filter/filter-list-helper';
import { Router } from '@angular/router';
import { TableFilterStateService } from 'src/app/shared/filter/table-filter-state.service';
import { ProjectService } from '../project.service';
import { FilterList } from 'src/app/shared/filter/filter-list';

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
    private projectService: ProjectService) {
    this.filterListHelper = new FilterListHelper(this, this.filterStateService);
    this.filterStateService.onSort('id', -1);
  }

  ngOnInit() {
    this.cols = [
      { field: 'name', header: 'Project Name' },
      { field: 'managerDisplayName', header: 'Manager' },
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
}
