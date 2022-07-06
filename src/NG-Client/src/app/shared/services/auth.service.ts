import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {


  private permissionList: string[] = []

  sub = new Subject<void>();

  $isAdmin = false;

  stateChange() {
    return this.sub.asObservable();
  }

  /**
   * 刷新数据
   * @param permission 
   */
  refreshPermission(permission: string[], isAdmin: boolean) {
    this.permissionList = []
    this.permissionList.push(...permission)
    this.$isAdmin = isAdmin
    this.sub.next();
  }

  /**
   * 检查是否有权限
   * @param group 
   * @param action 
   * @returns 
   */
  checkPermission(name: string) {
    if (this.$isAdmin) return true;
    return this.permissionList.indexOf(name) > -1
  }

  constructor() { }
}
