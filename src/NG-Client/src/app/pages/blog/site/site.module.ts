import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SiteRoutingModule } from './site-routing.module';
import { SiteSettingComponent } from './site-setting/site-setting.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { SettingContainerComponent } from './setting-container/setting-container.component';

import { CodemirrorModule } from '@ctrl/ngx-codemirror';


@NgModule({
  declarations: [
    SiteSettingComponent,
    SettingContainerComponent
  ],
  imports: [
    CommonModule,
    SiteRoutingModule,
    SharedModule,
    CodemirrorModule
  ]
})
export class SiteModule { }
