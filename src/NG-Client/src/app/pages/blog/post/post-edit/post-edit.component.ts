import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzUploadChangeParam, NzUploadFile } from 'ng-zorro-antd/upload';
import { finalize, Observable, Observer } from 'rxjs';
import { PCropperComponent } from 'src/app/components/panda/p-cropper/p-cropper.component';
import { CategoryService, PostService, TagService } from 'src/app/net';

@Component({
  selector: 'app-post-edit',
  templateUrl: './post-edit.component.html',
  styleUrls: ['./post-edit.component.less']
})
export class PostEditComponent implements OnInit {

  formGroup!: FormGroup;

  tags: string[] = []

  constructor(
    private cateService: CategoryService,
    private tagService: TagService,
    private fb: FormBuilder,
    private routeActivate: ActivatedRoute,
    private post: PostService,
    private message: NzMessageService,
    private router: Router,
    private modal: NzModalService
  ) {
    this.formGroup = this.fb.group({
      id: [0],
      tags: [[], [Validators.required]],
      title: ['', [Validators.required]],
      markdown: ['', [Validators.required, this.markdownValidator]],
      categories: [[], [Validators.required, Validators.minLength(1)]],
      cover: [''],
      status: [0]
    })
  }

  markdownValidator = (control: FormControl) => {
    let v = control.value as string;
    if (!v.trim()) {
      return {
        error: true,
        required: true
      }
    }
    return {}
  }



  // ValidatorCategory = (control: FormControl): { [s: string]: boolean } => {
  //   let selectedOne = [];

  //   for (const i in control.value) {
  //     if (control.value[i].checked) {
  //       selectedOne.push(control.value[i]);
  //     }
  //   }
  //   if (selectedOne.length == 0) {
  //     return { required: true };
  //   }
  //   return {}
  // }

  cateGroups: { label: string, value: number, checked?: boolean }[] = []

  loading = false

  ngOnInit(): void {
    this.getCateList()
    this.getTagList()
    this.routeActivate.queryParams.subscribe(res => {
      let id = res['id'];
      if (id) {
        this.loading = true;
        this.post.adminPostGetGet(id)
          .pipe(finalize(() => this.loading = false))
          .subscribe(res => {
            console.log(res.categories?.map(a => a.id))
            this.formGroup.patchValue({
              id: res.id,
              tags: res.tags,
              title: res.title,
              markdown: res.markDown,
              categories: res.categories?.map(a => a.id),
              cover: res.cover,
              status: res.status
            })
            this.coverUrl = res.cover
          })
      }
    })
  }

  cateList: { label: string, value: number, checked?: boolean }[] = [];

  getCateList() {
    this.cateService.adminCategoryGetListGet(1, 100).subscribe(res => {
      res.forEach(item => {

        this.cateList.push({
          label: item.categoryName!,
          value: item.id!
        })
      })
    })
  }

  getTagList() {
    this.tagService.adminTagGetListGet(1, 100).subscribe(res => {
      this.tags = []
      let tagList = res.data?.map(a => a.tagName);
      tagList?.forEach(tag => {
        this.tags.push(tag!)
      })
    })
  }


  coverUrl?: string;

  uploading = false

  uploadChange(event: NzUploadChangeParam) {

    if (event.type == 'success') {
      console.log(event)
      this.uploading = false
      if (event.file.response.code == 0) {
        this.coverUrl = event.file.response.url
        this.formGroup.patchValue({
          cover: this.coverUrl
        })
      }
    }
    if (event.type == 'start') {
      this.uploading = true;
    }
  }

  copperFile = (file: NzUploadFile) => {
    return new Observable((observer: Observer<string>) => {
      const reader = new FileReader();
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      reader.readAsDataURL(file as any);
      reader.onload = () => {
        let imgData = reader.result as string
        console.log(imgData)
        let modal = this.modal.create({
          nzContent: PCropperComponent,
          nzTitle: '选择封面图',
          nzComponentParams: {
            width: 200, height: 200,
            imgData: imgData
          }, nzFooter: null
        })
        modal.afterClose.subscribe((res: { success: Boolean, data: string }) => {
          if (res && res.success) {
            console.log('选择图片:' + res.data)
            observer.next(res.data)
            observer.complete()
          }
        })
      }
    })
  }

  saving = false

  saveDrafting = false;

  save() {
    this.formGroup.patchValue({
      status: 0
    })
    this.saving = true;
    console.log(this.formGroup.value)
    this.saveServer();
  }

  saveDraft() {
    this.formGroup.patchValue({
      status: 1
    })
    this.saveDrafting = true;
    this.saveServer();
  }


  saveServer() {
    this.post.adminPostAddOrUpdatePost(
      this.formGroup.value
    ).pipe(finalize(() => {
      this.saving = false
      this.saveDrafting = false
    })).subscribe(_ => {
      this.message.success('保存成功')
      this.formGroup.reset()
      this.backToList();
    })
  }

  backToList() {
    this.router.navigate(['/admin/blog/post'])
  }

}
