import { Component, OnInit } from '@angular/core';
import { AccountService } from './net';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent implements OnInit {

  constructor(private account:AccountService){
    account.adminAccountIsLoginGet().subscribe(res=>{
      if(res.isLogin){
        
      }
    })
  }

  ngOnInit(): void {

  }
  isCollapsed = false;
  
}
