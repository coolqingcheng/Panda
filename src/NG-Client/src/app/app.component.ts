import { Component, OnInit } from '@angular/core';
import { NavigationCancel, NavigationEnd, NavigationStart, Router } from '@angular/router';
import { AccountService } from './net';
import * as NProgress from "nprogress"
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent implements OnInit {

  constructor(private account: AccountService
    , private router: Router
  ) {
    account.adminAccountIsLoginGet().subscribe(res => {
      if (!res.isLogin) {
        this.router.navigate(['/auth/login'], {
          replaceUrl: true
        })
      }
    })
  }

  ngOnInit(): void {

    this.router.events.subscribe(event => {

      if (event instanceof NavigationStart) {
        console.log('路由开始', event.url)
        NProgress.start();
      }
      if (event instanceof NavigationEnd) {
        console.log('路由结束:', event.url)
        NProgress.done()
      }
      if (event instanceof NavigationCancel) {
        console.log('路由取消', event.reason)
        NProgress.done()
      }
    })

  }
  isCollapsed = false;

}
