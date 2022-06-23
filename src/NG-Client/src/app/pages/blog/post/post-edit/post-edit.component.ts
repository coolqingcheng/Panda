import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NzUploadChangeParam } from 'ng-zorro-antd/upload';
import { finalize } from 'rxjs';
import { CategoryService, PostService, TagService } from 'src/app/net';

@Component({
  selector: 'app-post-edit',
  templateUrl: './post-edit.component.html',
  styleUrls: ['./post-edit.component.less']
})
export class PostEditComponent implements OnInit {

  fromGroup!: FormGroup;

  tags = ['tag1', 'tag2']

  constructor(
    private cateService: CategoryService,
    private tagService: TagService,
    private fb: FormBuilder,
    private router: ActivatedRoute,
    private post: PostService
  ) {
    this.fromGroup = this.fb.group({
      id: [0],
      tags: [[], [Validators.required]],
      title: ['', [Validators.required]],
      markdown: ['', [Validators.required,]],
      categories: [[], [Validators.required, Validators.minLength(1)]]
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
    this.router.queryParams.subscribe(res => {
      let id = res['id'];
      if (id) {
        this.loading = true;
        this.post.adminPostGetGet(id)
          .pipe(finalize(() => this.loading = false))
          .subscribe(res => {
            console.log(res.categories?.map(a => a.id))
            this.fromGroup.patchValue({
              id: res.id,
              tags: res.tags,
              title: res.title,
              markdown: res.markDown,
              categories: res.categories?.map(a => a.id)
            })
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
      }
    }
    if (event.type == 'start') {
      this.uploading = true;
    }
  }


  save() {
    console.log(this.fromGroup.value)
  }

}
