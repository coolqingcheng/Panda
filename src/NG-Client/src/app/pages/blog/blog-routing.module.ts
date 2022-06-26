import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShareRouteOutComponent } from 'src/app/shared/share-route-out/share-route-out.component';

const routes: Routes = [
  {
    path: 'category', component: ShareRouteOutComponent,
    loadChildren: () => import("./category/category.module").then(m => m.CategoryModule)
  },
  {
    path: 'post', component: ShareRouteOutComponent,
    loadChildren: () => import("./post/post.module").then(m => m.PostModule)
  },
  {
    path: 'tag', component: ShareRouteOutComponent,
    loadChildren: () => import('./tags/tags.module').then(m => m.TagsModule)
  },
  {
    path: 'site', component: ShareRouteOutComponent,
    loadChildren: () => import('./site/site.module').then(m => m.SiteModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BlogRoutingModule { }
