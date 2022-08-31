import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ShareRouteOutComponent } from 'src/app/shared/share-route-out/share-route-out.component';
import { Page404Component } from './page404/page404.component';
import { StatisticComponent } from './statistic/statistic.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { WelcomeComponent } from './welcome.component';

const routes: Routes = [
  {
    path: 'index', component: StatisticComponent
  },
  {
    path: 'unauthorized', component: UnauthorizedComponent
  },
  {
    path: 'blog', component: ShareRouteOutComponent,
    loadChildren: () => import('../blog/blog.module').then(m => m.BlogModule)
  },
  {
    path: 'sys', component: ShareRouteOutComponent,
    loadChildren: () => import("../system/system.module").then(m => m.SystemModule)
  },
  {
    path: '**', component: Page404Component
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WelcomeRoutingModule { }
