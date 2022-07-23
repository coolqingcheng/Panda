import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-account-logs',
  templateUrl: './account-logs.component.html',
  styleUrls: ['./account-logs.component.less']
})
export class AccountLogsComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  @Input() id!: string

}
