import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { finalize } from 'rxjs';
import { FriendLinkService } from 'src/app/net';

@Component({
  selector: 'app-friendlink-edit',
  templateUrl: './friendlink-edit.component.html',
  styleUrls: ['./friendlink-edit.component.less']
})
export class FriendlinkEditComponent implements OnInit {

  @Input() id: number = 0;

  formGroup!: FormGroup

  constructor(
    private friend: FriendLinkService,
    private fb: FormBuilder,
    private modal: NzModalRef,
    private message: NzMessageService
  ) {
    this.formGroup = fb.group({
      id: [0],
      siteUrl: ['', [Validators.required]],
      siteName: ['', [Validators.required]],
      weight: [0, [Validators.required]],
      auditStatus: [0, [Validators.required]]
    })
  }

  loading = false
  ngOnInit(): void {
    console.log('id:' + this.id)
    if (this.id > 0) {
      this.loading = true
      this.friend.adminFriendLinkGetGet(this.id).pipe(finalize(() => this.loading = false))
        .subscribe(res => {
          res.addTime
          this.formGroup.patchValue(res)
        })
    }
  }

  close() {
    this.modal.close()
  }

  saving = false;

  save() {
    this.saving = true
    this.friend.adminFriendLinkAddOrUpdatePost(this.formGroup.value).pipe(finalize(() => {
      this.saving = false
    })).subscribe(() => {
      this.message.success('保存成功')
      this.modal.close('ok')
    })
  }
}
