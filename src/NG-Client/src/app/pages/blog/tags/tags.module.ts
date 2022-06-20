import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TagsRoutingModule } from './tags-routing.module';
import { TagListComponent } from './tag-list/tag-list.component';
import { TagEditComponent } from './tag-edit/tag-edit.component';


@NgModule({
  declarations: [
    TagListComponent,
    TagEditComponent
  ],
  imports: [
    CommonModule,
    TagsRoutingModule
  ]
})
export class TagsModule { }
