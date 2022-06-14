import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { IconsProviderModule } from 'src/app/icons-provider.module';

import { WelcomeRoutingModule } from './welcome-routing.module';

import { WelcomeComponent } from './welcome.component';
import { StatisticComponent } from './statistic/statistic.component';
import { AccessLogComponent } from './access-log/access-log.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { Page404Component } from './page404/page404.component';


@NgModule({
  imports: [
    WelcomeRoutingModule,
    HttpClientModule,
    IconsProviderModule,
    NzLayoutModule,
    NzMenuModule,
    SharedModule
  ],
  declarations: [WelcomeComponent, StatisticComponent, AccessLogComponent, Page404Component],
  exports: [WelcomeComponent]
})
export class WelcomeModule { }
