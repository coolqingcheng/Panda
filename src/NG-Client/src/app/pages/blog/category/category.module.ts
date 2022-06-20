import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoryRoutingModule } from './category-routing.module';
import { CategoryListComponent } from './category-list/category-list.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { CategoryEditComponent } from './category-edit/category-edit.component';


@NgModule({
  declarations: [
    CategoryListComponent,
    CategoryEditComponent
  ],
  imports: [
    CommonModule,
    CategoryRoutingModule,
    SharedModule
  ]
})
export class CategoryModule { }
