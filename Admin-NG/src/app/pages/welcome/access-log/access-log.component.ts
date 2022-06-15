import { Component, OnInit } from '@angular/core';
import { NzTableQueryParams } from 'ng-zorro-antd/table';
import { finalize } from 'rxjs';
import { RecentAccessHistory, SiteStatisticService, StatisticService } from 'src/app/net';
import { BaseTableComponent } from 'src/app/shared/BaseTableComponent';

@Component({
  selector: 'app-access-log',
  templateUrl: './access-log.component.html',
  styleUrls: ['./access-log.component.less']
})
export class AccessLogComponent extends BaseTableComponent implements OnInit {

  constructor(
    private statistic: StatisticService
  ) {
    super(() => {
      this.getData();
    })
  }

  ngOnInit(): void {
    this.getData();
  }



  dataSet: RecentAccessHistory[] = [];

  getData() {
    this.loading = true;
    this.statistic.adminStatisticGetRecentAccessRecordGet(this.page, this.size).pipe(finalize(() => {
      this.loading = false
    })).subscribe(res => {
      if (res.data) {
        this.dataSet = res.data
        this.total = res.total!
      }
    })
  }

}
