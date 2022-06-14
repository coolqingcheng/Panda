import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShareRouteOutComponent } from 'src/app/shared/share-route-out/share-route-out.component';

const routes: Routes = [
  {
    path: 'category', component: ShareRouteOutComponent, loadChildren: () => import("./category/category.module").then(m => m.CategoryModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BlogRoutingModule { }
