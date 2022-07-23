import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalService } from 'ng-zorro-antd/modal';
import { finalize } from 'rxjs';
import { SiteSettingService } from 'src/app/net';
import { SendTestEmailComponent } from './send-test-email/send-test-email.component';

@Component({
  selector: 'app-email-setting',
  templateUrl: './email-setting.component.html',
  styleUrls: ['./email-setting.component.less']
})
export class EmailSettingComponent implements OnInit {

  constructor(
    private fb: FormBuilder,
    private setting: SiteSettingService,
    private message: NzMessageService,
    private modal: NzModalService
  ) {
    this.formGroup = this.fb.group({
      nickName: ['', [Validators.required]],
      userName: ['', [Validators.required]],
      password: ['', [Validators.required]],
      host: ['', [Validators.required]],
      port: [0, [Validators.required]],
      useSSL: [true, [Validators.required]]
    })
  }

  loading = false

  formGroup!: FormGroup;

  ngOnInit(): void {
    this.loading = true;
    this.setting.adminSiteSettingGetEmailGet().pipe(finalize(() => this.loading = false))
      .subscribe(res => {
        console.log(res)
        this.formGroup.patchValue(res)
      })
  }

  saving = false;
  save() {
    if (!this.formGroup.valid) return;
    this.saving = true;
    this.setting.adminSiteSettingSetEmailPost(this.formGroup.value).pipe(finalize(() => this.saving = false))
      .subscribe(_ => {
        this.message.success('保存成功')
      })
  }

  createSendTest() {
    this.modal.create({
      nzTitle: '发送测试邮件',
      nzContent: SendTestEmailComponent,
      nzFooter: null
    })
  }

}
