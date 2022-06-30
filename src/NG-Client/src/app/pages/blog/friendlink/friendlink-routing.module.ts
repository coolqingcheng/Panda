import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FriendlinkEditComponent } from './friendlink-edit/friendlink-edit.component';
import { FriendlinkListComponent } from './friendlink-list/friendlink-list.component';

const routes: Routes = [
  {
    path: '', redirectTo: 'list'
  },
  {
    path: 'list', component: FriendlinkListComponent
  },
  {
    path: 'edit', component: FriendlinkEditComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FriendlinkRoutingModule { }
