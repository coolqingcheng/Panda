import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PVditorComponent } from './p-vditor/p-vditor.component';
import { PCropperComponent } from './p-cropper/p-cropper.component';



@NgModule({
  declarations: [
    PVditorComponent,
    PCropperComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    PVditorComponent
  ]
})
export class PandaModule { }
