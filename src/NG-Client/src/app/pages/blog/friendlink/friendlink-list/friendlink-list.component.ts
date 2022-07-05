import { Component, OnInit } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalService } from 'ng-zorro-antd/modal';
import { finalize } from 'rxjs';
import { FriendLinkService, FriendlyLinkResponse } from 'src/app/net';
import { BaseTableComponent } from 'src/app/shared/BaseTableComponent';
import { FriendlinkEditComponent } from '../friendlink-edit/friendlink-edit.component';

@Component({
  selector: 'app-friendlink-list',
  templateUrl: './friendlink-list.component.html',
  styleUrls: ['./friendlink-list.component.less']
})
export class FriendlinkListComponent extends BaseTableComponent implements OnInit {

  constructor(
    private friend: FriendLinkService,
    private modal: NzModalService,
    private message: NzMessageService
  ) {
    super(() => {
      this.init();
    })
  }

  ngOnInit(): void {
  }

  datas: FriendlyLinkResponse[] = []

  init() {
    this.loading = true
    this.friend.adminFriendLinkGetListGet(this.page, this.size).pipe(finalize(() => this.loading = false))
      .subscribe(res => {
        this.total = res.total!;
        this.datas = []
        this.datas.push(...res.data!)
      })
  }

  edit(id?: number) {
    let modal = this.modal.create({
      nzTitle: id ? '编辑' : "添加",
      nzContent: FriendlinkEditComponent,
      nzFooter: null,
      nzComponentParams: {
        id: id
      }
    })
    modal.afterClose.subscribe((res) => {
      if (res) {
        this.init();
      }
    })
  }

  del(item: FriendlyLinkResponse) {
    this.modal.confirm({
      nzTitle: '提示',
      nzContent: `确定删除【${item.siteName}】?`,
      nzOnOk: () => {
        let messageId = this.message.loading('删除中...', { nzDuration: 0 }).messageId
        this.friend.adminFriendLinkDeleteDelete(item.id).pipe(finalize(() => this.loading = false)).subscribe(() => {
          this.message.success('删除成功')
          this.init();
          this.message.remove(messageId)
        })
      }
    })
  }

}
