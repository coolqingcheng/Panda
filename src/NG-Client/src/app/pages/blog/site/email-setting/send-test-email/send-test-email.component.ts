import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { finalize } from 'rxjs';
import { EmailService } from 'src/app/net';

@Component({
  selector: 'app-send-test-email',
  templateUrl: './send-test-email.component.html',
  styleUrls: ['./send-test-email.component.less']
})
export class SendTestEmailComponent implements OnInit {

  formGroup!: FormGroup

  constructor(
    private fb: FormBuilder,
    private email: EmailService,
    private message: NzMessageService,
    private modelRef: NzModalRef
  ) {
    this.formGroup = this.fb.group({
      email: ['', [Validators.required, Validators.email]]
    })
  }

  ngOnInit(): void {
  }

  loading = false

  send() {
    if (!this.formGroup.valid) return
    this.loading = true;
    this.email.adminEmailSendTestPost(this.formGroup.value).pipe(finalize(() => this.loading = false))
      .subscribe(_ => {
        this.message.success('发送成功')
      })
  }

}
