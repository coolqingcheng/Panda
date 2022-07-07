import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NzMessageService } from 'ng-zorro-antd/message';
import { finalize } from 'rxjs';
import { AccountService, PermissionService } from 'src/app/net';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-auth-login',
  templateUrl: './auth-login.component.html',
  styleUrls: ['./auth-login.component.less']
})
export class AuthLoginComponent implements OnInit {

  loginFormGroup!: FormGroup;

  loading = false;

  constructor(
    private fb: FormBuilder,
    private account: AccountService,
    private router: Router,
    private message: NzMessageService,
    private permission: PermissionService,
    private auth: AuthService
  ) {
    this.loginFormGroup = this.fb.group({
      userName: [null, [Validators.required]],
      password: [null, [Validators.required]]
    })
  }

  ngOnInit(): void {
  }
  login() {
    if (this.loginFormGroup.valid) {
      this.loading = true;
      this.account.adminAccountLoginPost(this.loginFormGroup.value).pipe(finalize(() => this.loading = false)).subscribe(res => {
        let loading = this.message.loading("loading...")
        this.permission.adminPermissionGetPermissionsGet().pipe(finalize(() => this.message.remove(loading.messageId))).subscribe(res => {
          console.log(res)
          this.auth.refreshPermission(res.permissions!, res.isAdmin!)
          if (this.auth.checkPermission("用户管理-登录")) {
            this.message.success('登录成功')
            this.router.navigate(["/admin"], {
              replaceUrl: true
            })
          } else {
            this.message.error('当前账号没有登录权限')
          }
        })

      })
    } else {
      Object.values(this.loginFormGroup.controls).forEach(control => {
        control.markAsDirty();
        control.updateValueAndValidity({ onlySelf: true });
      })
    }
  }
}
