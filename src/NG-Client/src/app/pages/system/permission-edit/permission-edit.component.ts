import { Component, Input, OnInit } from '@angular/core';
import { NzTreeNode, NzTreeNodeOptions } from 'ng-zorro-antd/tree';
import { finalize } from 'rxjs';
import { PermissionGroup, PermissionService } from 'src/app/net';

@Component({
  selector: 'app-permission-edit',
  templateUrl: './permission-edit.component.html',
  styleUrls: ['./permission-edit.component.less']
})
export class PermissionEditComponent implements OnInit {

  @Input() id!: string;

  constructor(
    private permission: PermissionService
  ) { }

  ngOnInit(): void {

  }

  ngAfterViewInit() {
    this.init();
  }

  datas: PermissionGroup[] = []


  init() {
    this.permission.adminPermissionGetAllGet().pipe(finalize(() => {

    }))
      .subscribe(res => {
        this.datas = []
        this.datas.push(...res)
        this.toTreeData();
      })
  }

  treeData: NzTreeNodeOptions[] = [
  ]

  selectKey = []
  

  test(){
    this.treeData.push(
      {
        title: 'TEST', key: ''
      }
    )

    console.log(this.treeData)
  }

  toTreeData() {
    this.datas.forEach(item => {
      var node: NzTreeNodeOptions = {
        title: item.groupName!,
        key: item.key!,
        children: item.list!.map(a => {
          return {
            title: a.name!,
            key: a.key!,
            isLeaf:true
          }
        })
      }
      this.treeData.push(node)
    })


    console.log(this.treeData)
  }

}
