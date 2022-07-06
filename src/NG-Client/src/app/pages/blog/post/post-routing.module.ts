import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShareRouteOutComponent } from 'src/app/shared/share-route-out/share-route-out.component';
import { PostEditComponent } from './post-edit/post-edit.component';
import { PostListComponent } from './post-list/post-list.component';

const routes: Routes = [
  {
    path: '', redirectTo: 'list',

  },
  {
    path: 'list', component: PostListComponent,
    data: {
      auth: '文章管理-查看'
    }
  },
  {
    path: 'edit', component: PostEditComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PostRoutingModule { }
