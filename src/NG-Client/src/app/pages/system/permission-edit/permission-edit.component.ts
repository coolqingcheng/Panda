import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { NzTreeComponent, NzTreeNode, NzTreeNodeOptions } from 'ng-zorro-antd/tree';
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
    private permission: PermissionService,
    private modal: NzModalRef
  ) { }

  ngOnInit(): void {

  }

  ngAfterViewInit() {
    this.init();
  }

  datas: PermissionGroup[] = []

  checkedKeys: string[] = []

  selectKeys = []


  loading = false

  init() {
    this.loading = true;
    this.permission.adminPermissionGetAllGet().pipe(finalize(() => {
      this.loading = false
    }))
      .subscribe(res => {
        this.permission.adminPermissionGetPermissionsGet().subscribe(checkedKeys => {
          this.checkedKeys = checkedKeys.permissions!
          this.datas = []
          this.datas.push(...res)
          this.toTreeData();
        })
      })
  }

  treeData: NzTreeNodeOptions[] = [
  ]


  test() {
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
            key: a.key!
          }
        })
      }
      this.treeData.push(node)
    })


    console.log(this.treeData)
  }


  close() {
    this.modal.close()
  }

  @ViewChild("tree") tree!: NzTreeComponent

  save() {
    let checkedKeys: string[] = [];

    this.tree.getCheckedNodeList().map(a => a.children.map(b => b.key)).forEach(item => {
      checkedKeys.push(...item)
    });
    console.log(checkedKeys)
  }

}
