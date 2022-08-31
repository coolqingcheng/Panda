import { Component, OnInit } from '@angular/core';
import { PermissionService } from 'src/app/net';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.less']
})
export class WelcomeComponent implements OnInit {
  isCollapsed = false;
  constructor(
    private permission: PermissionService,
    private auth: AuthService,

  ) { }

  ngOnInit() {
    this.getPermission();
  }

  getPermission() {
    this.permission.adminPermissionGetPermissionsGet().subscribe(res => {
      this.auth.refreshPermission(res.permissions!, res.isAdmin!)
    })
  }

}
