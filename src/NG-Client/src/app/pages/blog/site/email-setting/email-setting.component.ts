import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd/message';
import { finalize } from 'rxjs';
import { SiteSettingService } from 'src/app/net';

@Component({
  selector: 'app-email-setting',
  templateUrl: './email-setting.component.html',
  styleUrls: ['./email-setting.component.less']
})
export class EmailSettingComponent implements OnInit {

  constructor(
    private fb:FormBuilder,
    private site:SiteSettingService,
    private message:NzMessageService
  ) { 
    this.formGroup = this.fb.group({
      nickName:['',[Validators.required]],
      userName:['',[Validators.required]],
      password:['',[Validators.required]],
      host:['',[Validators.required]],
      port:[0,[Validators.required]],
      useSSL:[true,[Validators.required]]
    })
  }

  loading = false

  formGroup!:FormGroup;

  ngOnInit(): void {
    this.loading = true;
    this.site.adminSiteSettingGetEmailGet().pipe(finalize(()=>this.loading = false))
    .subscribe(res=>{
      console.log(res)
      this.formGroup.patchValue(res)
    })
  }


  save(){
    if(!this.formGroup.valid)return;
    this.site.adminSiteSettingSetEmailPost(this.formGroup.value).pipe(finalize(()=>this.loading = false))
    .subscribe(_=>{
      this.message.success('保存成功')
    })
  }

}
