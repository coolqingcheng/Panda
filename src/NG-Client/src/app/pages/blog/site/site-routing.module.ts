import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/shared/routers/auth.guard';
import { SettingContainerComponent } from './setting-container/setting-container.component';
import { SiteSettingComponent } from './site-setting/site-setting.component';

const routes: Routes = [
  {
    path: '', component: SettingContainerComponent, data: {
      auth: '网站设置-站点设置'
    }, canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SiteRoutingModule { }
