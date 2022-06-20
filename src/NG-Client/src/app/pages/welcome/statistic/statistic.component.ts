import { Component, OnInit } from '@angular/core';
import { DateType, SiteStatisticModel, SiteStatisticService } from 'src/app/net';

@Component({
  selector: 'app-statistic',
  templateUrl: './statistic.component.html',
  styleUrls: ['./statistic.component.less']
})
export class StatisticComponent implements OnInit {

  constructor(private statistic: SiteStatisticService) { }

  dataSet = []

  model?:SiteStatisticModel;

  ngOnInit(): void {

    this.statistic.adminSiteStatisticGetStatisticCollectGet(DateType.NUMBER_2)
    .subscribe(res=>{
      console.log('调用')
      console.log(res)
      this.model = res
    })
  }

}
