import { Component, OnInit } from '@angular/core';
import { TagResponse, TagResponsePageDto, TagService } from 'src/app/net';
import { BaseTableComponent } from 'src/app/shared/BaseTableComponent';

@Component({
  selector: 'app-tag-list',
  templateUrl: './tag-list.component.html',
  styleUrls: ['./tag-list.component.less']
})
export class TagListComponent extends BaseTableComponent implements OnInit {

  constructor(
    private tag:TagService
  ) { 
    super(()=>{
      this.init();
    })
  }

  ngOnInit(): void {
    this.init();
  }

  datas:TagResponse[] = []
  init(){

    this.tag.adminTagGetListGet(this.page,this.size).subscribe(res=>{
      this.datas = [];
      this.datas.push(...res.data!)
      this.total = res.total!
    })
  }

}
