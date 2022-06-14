import { Component, OnInit } from '@angular/core';
import { SiteStatisticService } from 'src/app/net';

@Component({
  selector: 'app-statistic',
  templateUrl: './statistic.component.html',
  styleUrls: ['./statistic.component.less']
})
export class StatisticComponent implements OnInit {

  constructor(private statistic: SiteStatisticService) { }

  dataSet = []

  ngOnInit(): void {

    this.statistic.adminSiteStatisticGetStatisticCollectGet().subscribe(res=>{
      console.log('调用')
      console.log(res)
    })
  }

}
