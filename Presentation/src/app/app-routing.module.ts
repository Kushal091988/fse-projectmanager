import { Routes, RouterModule } from '@angular/router';
import { MenuService } from './core/menu/menu.service';
import { LayoutComponent } from './layout/layout.component';
import { NgModule } from '@angular/core';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: 'user', loadChildren: './users/user.module#UserModule1' },
      { path: 'project', loadChildren: './project/project.module#ProjectModule' },
      { path: 'task', loadChildren: './task/task.module#TaskModule' },
      { path: '', redirectTo: 'user', pathMatch: 'full' },
      { path: '**', redirectTo: 'user', pathMatch: 'full' }
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule],
  declarations: []
})
export class AppRoutingModule {
  constructor(private menuService: MenuService) {
    this.menuService.addMenu();
  }
}
