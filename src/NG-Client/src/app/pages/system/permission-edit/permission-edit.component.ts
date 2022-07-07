import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd/message';
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
    private modal: NzModalRef,
    private message: NzMessageService
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
    this.permission.adminPermissionGetAllGet(this.id).pipe(finalize(() => {
      this.loading = false
    }))
      .subscribe(res => {

        this.datas = []
        this.datas.push(...res)
        this.toTreeData();
      })
  }

  treeData: NzTreeNodeOptions[] = [
  ]

  toTreeData() {
    this.datas.forEach(item => {
      var node: NzTreeNodeOptions = {
        title: item.groupName!,
        key: item.key!,
        children: item.list!.map(a => {
          if (a.isGrant) {
            this.checkedKeys.push(a.key!)
          }
          return {
            title: a.name!,
            key: a.key!,
            isLeaf: true
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

  saving = false

  save() {
    let checkedKeys: string[] = [];

    this.tree.getCheckedNodeList().map(a => a.children.map(b => b.key)).forEach(item => {
      checkedKeys.push(...item)
    });
    console.log(checkedKeys)
    this.saving = true
    this.permission.adminPermissionAccountSetPermissionPost({
      accountId: this.id,
      permissionKeys: checkedKeys
    })
      .pipe(finalize(() => this.saving = false))
      .subscribe(_ => {
        this.message.success('保存成功')
        this.modal.close(true)
      })
  }

}
