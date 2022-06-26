import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SiteSettingComponent } from './site-setting/site-setting.component';

const routes: Routes = [
  {
    path:'',component:SiteSettingComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SiteRoutingModule { }
