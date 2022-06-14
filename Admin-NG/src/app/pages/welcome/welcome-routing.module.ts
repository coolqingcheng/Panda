import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ShareRouteOutComponent } from 'src/app/shared/share-route-out/share-route-out.component';
import { Page404Component } from './page404/page404.component';
import { StatisticComponent } from './statistic/statistic.component';
import { WelcomeComponent } from './welcome.component';

const routes: Routes = [
  {
    path: '', component: WelcomeComponent, children: [
      {
        path: "", component: StatisticComponent
      }
    ]
  },
  {
    path: 'blog', component:WelcomeComponent,
    loadChildren: () => import('../blog/blog.module').then(m => m.BlogModule)
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
