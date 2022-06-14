import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzDividerModule } from 'ng-zorro-antd/divider'

import { NzFormModule } from "ng-zorro-antd/form"
import { NzInputModule } from "ng-zorro-antd/input"
import { NzMessageModule } from "ng-zorro-antd/message"

import { NzStatisticModule } from 'ng-zorro-antd/statistic';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { AlertOutline, PlusOutline, SettingOutline } from '@ant-design/icons-angular/icons';
import { BASE_PATH } from '../net';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { SharedHttpInterceptor } from './SharedHttpInterceptor';
import { ShareRouteOutComponent } from './share-route-out/share-route-out.component';
import { RouterModule } from '@angular/router';

const icons = [
  AlertOutline, PlusOutline, SettingOutline
]

const antd = [
  NzLayoutModule,
  NzMenuModule,
  NzSpinModule,
  NzSelectModule,
  NzButtonModule,
  NzGridModule,
  NzCardModule,
  NzStatisticModule, NzTableModule, NzDividerModule,
  NzFormModule,
  NzInputModule,
  NzMessageModule
]

const common = [
  CommonModule, FormsModule, ReactiveFormsModule,RouterModule
]

@NgModule({
  declarations: [
    ShareRouteOutComponent
  ],
  imports: [
    ...common,
    ...antd,
    NzIconModule.forRoot(icons)
  ],
  exports: [...antd, ...common],
  providers: [
    { provide: BASE_PATH, useValue: " " },
  ]
})
export class SharedModule { }