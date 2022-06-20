import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NzMessageService } from 'ng-zorro-antd/message';
import { finalize } from 'rxjs';
import { AccountService } from 'src/app/net';

@Component({
  selector: 'app-auth-init',
  templateUrl: './auth-init.component.html',
  styleUrls: [
    './auth-init.component.less'
  ]
})
export class AuthInitComponent implements OnInit {

  loading = false;

  formGroup: FormGroup;

  constructor(
    private fb: FormBuilder,
    private account: AccountService,
    private message: NzMessageService,
    private router: Router
  ) {

    this.formGroup = this.fb.group({
      email: ['qqq@qq.com', [Validators.required, Validators.email]],
      userName: ['qq', [Validators.required]],
      pwd: [null, [Validators.required, this.pwdValid]],
      rePwd: [null, [Validators.required, this.pwdValid]],

    })
  }

  ngOnInit(): void {
  }
  pwdValid = (control: FormControl) => {
    console.log(control)
    if (!control.value) return
    let pwd = this.formGroup.controls['pwd'].value
    let confirm = this.formGroup.controls['rePwd'].value;
    if (confirm !== pwd) {
      return {
        error: true,
        pwdValid: true
      }
    }
    return {};
  }

  updateValidate() {
    this.formGroup.controls['pwd'].updateValueAndValidity();
    this.formGroup.controls['rePwd'].updateValueAndValidity();
  }



  regist() {
    this.loading = true
    this.account.adminCreateAdminAccountPost(this.formGroup.value).pipe(finalize(() => {
      this.loading = false
    })).subscribe(_ => {
      this.message.success('初始化账号成功')
      this.router.navigate(['/auth/login'])
    })
  }
}
