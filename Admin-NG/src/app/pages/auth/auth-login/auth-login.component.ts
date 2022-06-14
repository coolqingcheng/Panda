import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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

  constructor(private fb: FormBuilder, private account: AccountService) {
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

      })
    } else {
      Object.values(this.loginFormGroup.controls).forEach(control => {
        control.markAsDirty();
        control.updateValueAndValidity({ onlySelf: true });
      })
    }
  }
}
