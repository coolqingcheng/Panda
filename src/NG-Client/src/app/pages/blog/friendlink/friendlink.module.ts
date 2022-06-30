import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FriendlinkRoutingModule } from './friendlink-routing.module';
import { FriendlinkListComponent } from './friendlink-list/friendlink-list.component';
import { FriendlinkEditComponent } from './friendlink-edit/friendlink-edit.component';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
  declarations: [
    FriendlinkListComponent,
    FriendlinkEditComponent
  ],
  imports: [
    CommonModule,
    FriendlinkRoutingModule,
    SharedModule
  ]
})
export class FriendlinkModule { }
