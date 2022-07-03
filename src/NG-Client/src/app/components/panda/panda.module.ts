import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PVditorComponent } from './p-vditor/p-vditor.component';
import { PCropperComponent } from './p-cropper/p-cropper.component';

import { NzSpinComponent, NzSpinModule } from 'ng-zorro-antd/spin';
import { PSearchBoxComponent } from './p-search-box/p-search-box.component'



@NgModule({
  declarations: [
    PVditorComponent,
    PCropperComponent,
    PSearchBoxComponent
  ],
  imports: [
    CommonModule,
    NzSpinModule
  ],
  exports: [
    PVditorComponent,
    PSearchBoxComponent
  ]
})
export class PandaModule { }
