import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd/message';
import { finalize } from 'rxjs';
import { PostService, SiteSettingService } from 'src/app/net';
@Component({
  selector: 'app-site-setting',
  templateUrl: './site-setting.component.html',
  styleUrls: ['./site-setting.component.less']
})
export class SiteSettingComponent implements OnInit {


  formGroup!: FormGroup;

  options = {
    lineNumbers: true,
    readOnly: false, // nocursor can not copy
    mode: 'htmlembedded',
    autofocus: false,
    lineWiseCopyCut: true,
    cursorBlinkRate: 500 // hide cursor
  };

  constructor(
    private fb: FormBuilder,
    private setting: SiteSettingService,

    private message: NzMessageService
  ) {
    this.formGroup = this.fb.group({
      siteName: ['', [Validators.required]],
      siteDesc: ['', [Validators.required]],
      siteHost: ['', [Validators.required]],
      icpNo: [null, [Validators.required]],
      statisticCode: ['', [Validators.required]]
    })
  }

  ngOnInit(): void {
    this.init()
  }

  loading = false

  init() {
    this.loading = true;
    this.setting.adminSiteSettingGetSiteInfoGet().pipe(finalize(() => this.loading = false)).subscribe(res => {
      this.formGroup.patchValue(res)
    })
  }

  saving = false;

  save() {
    this.saving = true
    this.setting.adminSiteSettingSetSiteInfoPost(this.formGroup.value)
      .pipe(finalize(() => this.saving = false)).subscribe(() => {
        this.message.success('保存成功')
      })
  }

}
