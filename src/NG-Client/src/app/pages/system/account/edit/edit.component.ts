import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { finalize } from 'rxjs';
import { AccountService } from 'src/app/net';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.less']
})
export class EditComponent implements OnInit {

  formGroup!: FormGroup

  constructor(
    private fb: FormBuilder, private account: AccountService,
    private modal:NzModalRef,private message:NzMessageService
  ) {
    this.formGroup = this.fb.group({
      email: ['', [Validators.required,Validators.email]],
      userName: ['', [Validators.required]],
      password: ['', [Validators.required]]
    })
  }


  @Input() id: string | undefined = undefined

  ngOnInit(): void {
  }

  saving = false


  save() {
    this.saving = true;
    this.account.adminAccountAddOrUpdatePost(this.formGroup.value).pipe(finalize(()=>this.saving = false)).subscribe(_=>{
      this.message.success('保存成功')
      this.modal.close(true)
    })
  }

  close(){
    this.modal.close();
  }

}
