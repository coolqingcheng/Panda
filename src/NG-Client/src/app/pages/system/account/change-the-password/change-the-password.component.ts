import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { finalize, Observable } from 'rxjs';
import { AccountService } from 'src/app/net';

@Component({
  selector: 'app-change-the-password',
  templateUrl: './change-the-password.component.html',
  styleUrls: ['./change-the-password.component.less']
})
export class ChangeThePasswordComponent implements OnInit {

  @Input() id!: string

  formGroup!: FormGroup;

  constructor(
    private account: AccountService,
    private fb: FormBuilder,
    private message: NzMessageService,
    private modal: NzModalRef
  ) {
    this.formGroup = this.fb.group({
      accountId: [this.id, [Validators.required]],
      newPassword: ['', [Validators.required,Validators.minLength(8)]],
      confirmPwd: ['', [Validators.required, this.confirmPwdValidator]]
    })
  }

  ngOnInit(): void {
    this.formGroup.patchValue({
      accountId: this.id
    })
  }

  saving = false;


  save() {

    console.log(this.formGroup.value)
    if (this.formGroup.valid && this.saving == false) {
      this.saving = true;
      this.account.adminAccountChangeAccountPwdPost(this.formGroup.value).pipe(finalize(() => {
        this.saving = false
      })).subscribe(() => {
        this.message.success('修改密码成功')
        this.modal.close("ok")
      })
    }
  }

  updateValidateStatus() {
    console.log(this.formGroup.errors)
  }

  confirmPwdValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
    let confirmPwd = control?.value;
    if (!confirmPwd) return {};
    let newPassword = this.formGroup.get('newPassword')?.value
    // console.log(`newPassword:`, newPassword, 'newPwd', confirmPwd)
    if (newPassword != confirmPwd) {
      return {
        confirm: true,
        error: true
      }
    }
    return {}
  }

  close() {
    this.modal.close()
  }



}
