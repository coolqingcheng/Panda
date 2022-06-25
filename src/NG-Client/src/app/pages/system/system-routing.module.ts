import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShareRouteOutComponent } from 'src/app/shared/share-route-out/share-route-out.component';

const routes: Routes = [
  {
    path: 'account', component: ShareRouteOutComponent,
    loadChildren: () => import('./account/account.module').then(m => m.AccountModule)
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SystemRoutingModule { }
