import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PostRoutingModule } from './post-routing.module';
import { PostListComponent } from './post-list/post-list.component';
import { PostEditComponent } from './post-edit/post-edit.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { PandaModule } from 'src/app/components/panda/panda.module';


@NgModule({
  declarations: [
    PostListComponent,
    PostEditComponent
  ],
  imports: [
    CommonModule,
    PostRoutingModule,
    SharedModule,
    PandaModule
  ]
})
export class PostModule { }
