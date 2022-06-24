import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PVditorComponent } from './p-vditor/p-vditor.component';
import { PCropperComponent } from './p-cropper/p-cropper.component';

import { NzSpinComponent, NzSpinModule } from 'ng-zorro-antd/spin'



@NgModule({
  declarations: [
    PVditorComponent,
    PCropperComponent
  ],
  imports: [
    CommonModule,
    NzSpinModule
  ],
  exports: [
    PVditorComponent
  ]
})
export class PandaModule { }
