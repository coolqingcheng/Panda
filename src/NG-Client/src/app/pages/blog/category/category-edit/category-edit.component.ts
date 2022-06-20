import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { finalize } from 'rxjs';
import { CategoryService } from 'src/app/net';

@Component({
  selector: 'app-category-edit',
  templateUrl: './category-edit.component.html',
  styleUrls: ['./category-edit.component.less']
})
export class CategoryEditComponent implements OnInit {

  formGroup!: FormGroup;

  loading = false;

  @Input()
  id: number = 0;

  constructor(
    private fb: FormBuilder
    , private category: CategoryService,
    private message: NzMessageService,
    private modal: NzModalRef
  ) {

    this.formGroup = fb.group({
      id: [this.id],
      categoryName: [null, [Validators.required]],
      categoryDesc: [null],
      pid: [0],
      isShow: [true],
    })

  }

  ngOnInit(): void {
    console.log("id:" + this.id)
    if (this.id > 0) {
      this.loading = true
      this.category.adminCategoryGetGet(this.id).pipe(finalize(() => {
        this.loading = false
      })).subscribe(res => {
        this.formGroup.setValue({
          id: res.id, categoryName: res.categoryName,
          isShow: res.isShow,
          categoryDesc:res.categoryDesc,
          pid:res.pid
        })
      })
    }
  }

  save() {
    this.loading = true
    this.category.adminCategoryAddOrUpdatePost(this.formGroup.value).pipe(finalize(() => {
      this.loading = false
    })).subscribe(res => {
      this.message.success('保存成功')
      this.close();
    })
  }

  close() {
    this.modal.close();
  }

}
