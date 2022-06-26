import { Component, OnInit } from '@angular/core';
import { NzModalService } from 'ng-zorro-antd/modal';
import { finalize } from 'rxjs';
import { AccountResp, AccountRespPageDto, AccountService } from 'src/app/net';
import { BaseTableComponent } from 'src/app/shared/BaseTableComponent';
import { ChangeThePasswordComponent } from '../change-the-password/change-the-password.component';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.less']
})
export class ListComponent extends BaseTableComponent implements OnInit {

  constructor(
    private account: AccountService,
    private modal: NzModalService
  ) {

    super(() => {
      this.init()
    })
  }

  ngOnInit(): void {
    this.init();
  }

  datas: AccountResp[] = []


  init() {
    this.loading = true;
    this.account.adminAccountGetAccountListGet(this.page, this.size)
      .pipe(finalize(() => this.loading = false)).subscribe(res => {
        this.datas = []
        this.datas.push(...res.data!)
        this.total = res.total!;

      })
  }

  changePwd(id: string) {
    let modal =  this.modal.create({
      nzTitle: '修改密码',
      nzFooter: null,
      nzContent: ChangeThePasswordComponent,
      nzKeyboard: false,
      nzComponentParams: {
        id: id
      }
    })
    modal.afterClose.subscribe(res=>{
      if(res){
        this.init();
      }
    })
  }
}
