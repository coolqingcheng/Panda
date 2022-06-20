import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NzMessageService } from 'ng-zorro-antd/message';
import { finalize } from 'rxjs';
import { AccountService } from 'src/app/net';

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
    private message: NzMessageService
  ) {
    this.loginFormGroup = this.fb.group({
      userName: [null, [Validators.required]],
      password: [null, [Validators.required]]
    })
  }

  ngOnInit(): void {
  }
  login() {
    console.log('login:' + this.loginFormGroup.valid)
    if (this.loginFormGroup.valid) {
      console.log('验证通过')
      this.loading = true;
      this.account.adminAccountLoginPost(this.loginFormGroup.value).pipe(finalize(() => this.loading = false)).subscribe(res => {
        this.message.success('登录成功')
        this.router.navigate(["/admin"], {
          replaceUrl: true
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
