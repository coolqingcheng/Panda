import { Component, OnInit } from '@angular/core';
import { finalize } from 'rxjs';
import { CategoryItem, CategoryService } from 'src/app/net';
import { BaseTableComponent } from 'src/app/shared/BaseTableComponent';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.less']
})
export class CategoryListComponent extends BaseTableComponent implements OnInit {

  constructor(
    private categoryService: CategoryService
  ) {
    super(() => {
      this.getData();
    })
  }

  ngOnInit(): void {
    this.getData();
  }

  dataSet: Array<CategoryItem> = []

  getData() {
    this.loading = true;
    this.categoryService.adminCategoryGetListGet(this.page, this.size).pipe(finalize(() => {
      this.loading = false
    })).subscribe(res => {
      this.dataSet = res;
    })
  }

}
