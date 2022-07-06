import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { PermissionService } from '../services/permission.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private auth: PermissionService,
    private router: Router
  ) {

  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    let str = route.data['auth']
    console.log('canActivate：' + str)
    if (this.auth.checkPermission(str) == false) {
      this.router.navigate(['/admin/unauthorized'], {
        replaceUrl: true
      })
      return false;
    } else {
      return true;
    }

  }

}
