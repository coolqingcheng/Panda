import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PVditorComponent } from './p-vditor/p-vditor.component';
import { PCropperComponent } from './p-cropper/p-cropper.component';

import { NzSpinComponent, NzSpinModule } from 'ng-zorro-antd/spin';
import { PSearchBoxComponent } from './p-search-box/p-search-box.component';
import { PFormSubmitBoxComponent } from './p-form-submit-box/p-form-submit-box.component'



@NgModule({
  declarations: [
    PVditorComponent,
    PCropperComponent,
    PSearchBoxComponent,
    PFormSubmitBoxComponent
  ],
  imports: [
    CommonModule,
    NzSpinModule
  ],
  exports: [
    PVditorComponent,
    PSearchBoxComponent,PFormSubmitBoxComponent
  ]
})
export class PandaModule { }
