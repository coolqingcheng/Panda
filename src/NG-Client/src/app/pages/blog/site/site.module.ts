import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SiteRoutingModule } from './site-routing.module';
import { SiteSettingComponent } from './site-setting/site-setting.component';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
  declarations: [
    SiteSettingComponent
  ],
  imports: [
    CommonModule,
    SiteRoutingModule,
    SharedModule
  ]
})
export class SiteModule { }
