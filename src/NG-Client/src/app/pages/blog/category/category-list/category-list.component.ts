import { Component, OnInit } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalService } from 'ng-zorro-antd/modal';
import { finalize } from 'rxjs';
import { CategoryItem, CategoryService } from 'src/app/net';
import { BaseTableComponent } from 'src/app/shared/BaseTableComponent';
import { CategoryEditComponent } from '../category-edit/category-edit.component';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.less']
})
export class CategoryListComponent extends BaseTableComponent implements OnInit {

  constructor(
    private categoryService: CategoryService,
    private modal: NzModalService,
    private msg: NzMessageService
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
    this.size = 100;
    this.categoryService.adminCategoryGetListGet(this.page, this.size).pipe(finalize(() => {
      this.loading = false
    })).subscribe(res => {
      this.dataSet = res;
    })
  }

  edit(id:number) {
    this.modal.create({
      nzTitle: '添加分类',
      nzContent: CategoryEditComponent,
      nzFooter: null, nzKeyboard: false,
      nzComponentParams:{
        id:id
      }
    })
  }

  del(id: number) {
    this.modal.confirm({
      nzTitle: '提示',
      nzContent: '确定删除吗？',
      nzOnOk: () => {
        let loadingRef = this.msg.loading('删除中')
        this.categoryService.adminCategoryDeleteDelete(id).pipe(finalize(() => {
          this.msg.remove(loadingRef.messageId)
        })).subscribe(_ => {
          this.msg.success('删除成功')
          this.getData();
        })
      }
    })
  }

}
