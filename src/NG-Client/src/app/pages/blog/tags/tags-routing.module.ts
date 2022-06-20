import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TagListComponent } from './tag-list/tag-list.component';

const routes: Routes = [
  {
    path:'list',component:TagListComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TagsRoutingModule { }
