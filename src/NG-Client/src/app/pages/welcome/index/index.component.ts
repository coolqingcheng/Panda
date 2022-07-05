import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/net';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.less']
})
export class IndexComponent implements OnInit {

  constructor(
    private account: AccountService,
    private router: Router
  ) { }

  message = '页面加载中..'

  ngOnInit(): void {
    this.account.adminAccountIsLoginGet().subscribe(res => {
      this.router.navigate(['/admin'], {
        replaceUrl: true
      })
    }, err => {
      this.message = '网络连接失败...'
      console.log('错误执行。。。。',err)
    })
  }

}
