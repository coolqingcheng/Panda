import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SystemRoutingModule } from './system-routing.module';
import { PermissionEditComponent } from './permission-edit/permission-edit.component';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
  declarations: [
    PermissionEditComponent
  ],
  imports: [
    CommonModule,
    SystemRoutingModule,
    SharedModule
  ]
})
export class SystemModule { }
