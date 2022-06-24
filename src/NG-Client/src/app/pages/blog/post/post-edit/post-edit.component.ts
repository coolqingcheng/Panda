import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzUploadChangeParam } from 'ng-zorro-antd/upload';
import { finalize } from 'rxjs';
import { CategoryService, PostService, TagService } from 'src/app/net';

@Component({
  selector: 'app-post-edit',
  templateUrl: './post-edit.component.html',
  styleUrls: ['./post-edit.component.less']
})
export class PostEditComponent implements OnInit {

  formGroup!: FormGroup;

  tags:string[] = []

  constructor(
    private cateService: CategoryService,
    private tagService: TagService,
    private fb: FormBuilder,
    private routeActivate: ActivatedRoute,
    private post: PostService,
    private message:NzMessageService,
    private router:Router
  ) {
    this.formGroup = this.fb.group({
      id: [0],
      tags: [[], [Validators.required]],
      title: ['', [Validators.required]],
      markdown: ['', [Validators.required,]],
      categories: [[], [Validators.required, Validators.minLength(1)]],
      cover:['']
    })
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
              cover:res.cover
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
          cover:this.coverUrl
        })
      }
    }
    if (event.type == 'start') {
      this.uploading = true;
    }
  }

  saving = false

  save() {
    this.saving = true;
    console.log(this.formGroup.value)
    this.post.adminPostAddOrUpdatePost(
      this.formGroup.value
    ).pipe(finalize(()=>this.saving = false)).subscribe(_=>{
      this.message.success('保存成功')
      this.formGroup.reset()
      this.router.navigate(['/admin/blog/post'])
    })
  }

}
