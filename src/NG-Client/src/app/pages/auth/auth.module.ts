import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsModule, ReactiveFormsModule } from '@angular/forms'

import { AuthRoutingModule } from './auth-routing.module';
import { AuthInitComponent } from './auth-init/auth-init.component';
import { AuthLoginComponent } from './auth-login/auth-login.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { PVditorComponent } from 'src/app/components/panda/p-vditor/p-vditor.component';
import { PandaModule } from 'src/app/components/panda/panda.module';


@NgModule({
  declarations: [
    AuthInitComponent,
    AuthLoginComponent
  ],
  imports: [
    CommonModule,
    AuthRoutingModule,
    SharedModule,
    PandaModule
  ]
})
export class AuthModule { }
