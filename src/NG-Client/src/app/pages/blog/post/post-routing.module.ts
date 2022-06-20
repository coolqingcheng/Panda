import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShareRouteOutComponent } from 'src/app/shared/share-route-out/share-route-out.component';
import { PostListComponent } from './post-list/post-list.component';

const routes: Routes = [
  {
    path: '', redirectTo: 'list',

  },
  {
    path: 'list', component: PostListComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PostRoutingModule { }
