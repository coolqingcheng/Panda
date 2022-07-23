import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountRoutingModule } from './account-routing.module';
import { ListComponent } from './list/list.component';
import { EditComponent } from './edit/edit.component';
import { ChangeThePasswordComponent } from './change-the-password/change-the-password.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { AccountLogsComponent } from './account-logs/account-logs.component';
import { LoginLogsComponent } from './account-logs/login-logs/login-logs.component';
import { ActionLogsComponent } from './account-logs/action-logs/action-logs.component';


@NgModule({
  declarations: [
    ListComponent,
    EditComponent,
    ChangeThePasswordComponent,
    AccountLogsComponent,
    LoginLogsComponent,
    ActionLogsComponent
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    SharedModule
  ]
})
export class AccountModule { }
