import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthInitComponent } from './auth-init/auth-init.component';
import { AuthLoginComponent } from './auth-login/auth-login.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'login' },
  {
    path: 'login', component: AuthLoginComponent
  },
  {
    path: 'init', component: AuthInitComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
