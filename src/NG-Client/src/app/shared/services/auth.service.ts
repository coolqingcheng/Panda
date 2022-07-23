import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {


  private permissionList: string[] = []

  sub = new Subject<void>();

  $isAdmin = false;

  serverLoad = false;

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
    let model = {
      isadmin: isAdmin,
      permission: permission
    }
    localStorage.setItem('auth', JSON.stringify(model))
    this.$isAdmin = isAdmin
    this.serverLoad = true;
    this.sub.next();
  }

  /**
   * 检查是否有权限
   * @param group 
   * @param action 
   * @returns 
   */
  checkPermission(name: string) {
    if (this.serverLoad == false) {
      let item = localStorage.getItem('auth')
      if (item) {
        var model = JSON.parse(item) as { isadmin: boolean, permission: string[] }
        this.$isAdmin = model.isadmin
        this.permissionList = []
        this.permissionList.push(...model.permission)
      }
    }
    if (this.$isAdmin) return true;
    return this.permissionList.indexOf(name) > -1
  }

  constructor() { }
}
