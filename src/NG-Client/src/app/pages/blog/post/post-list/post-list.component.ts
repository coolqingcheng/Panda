import { Component, OnInit } from '@angular/core';
import { finalize } from 'rxjs';
import { AdminCategoryItem, AdminPostItemResponse, PostService } from 'src/app/net';
import { BaseTableComponent } from 'src/app/shared/BaseTableComponent';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.less']
})
export class PostListComponent extends BaseTableComponent implements OnInit {


  dataList: Array<AdminPostItemResponse> = []

  constructor(
    private post: PostService
  ) {
    super(() => {
      this.getData();
    })
  }

  ngOnInit(): void {
    this.getData();
  }


  getData() {
    console.log('getData')
    this.loading = true
    this.post.adminPostGetListGet(this.page, this.size).pipe(finalize(() => {
      this.loading = false;
    })).subscribe(res => {
      this.total = res.total!
      this.dataList = res.data!
    })
  }

  getCate(arr: AdminCategoryItem[] | undefined) {
    if (arr) {

    }

  }

}
