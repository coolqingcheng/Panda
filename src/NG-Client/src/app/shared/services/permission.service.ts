import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PermissionService {


  private permissionList: string[] = []

  sub = new Subject<void>();

  stateChange() {
    return this.sub.asObservable();
  }

  /**
   * 刷新数据
   * @param permission 
   */
  refreshPermission(permission: string[]) {
    this.permissionList = []
    this.permissionList.push(...permission)
    this.sub.next();
  }

  /**
   * 检查是否有权限
   * @param group 
   * @param action 
   * @returns 
   */
  checkPermission(name: string) {
    return this.permissionList.indexOf(name) > -1
  }

  constructor() { }
}
