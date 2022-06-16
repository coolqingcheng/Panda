import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from './net';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent implements OnInit {

  constructor(private account:AccountService
    ,private router:Router
    ){
    account.adminAccountIsLoginGet().subscribe(res=>{
      if(!res.isLogin){
        this.router.navigate(['/auth/login'],{
          replaceUrl:true
        })
      }
    })
  }

  ngOnInit(): void {

  }
  isCollapsed = false;
  
}
