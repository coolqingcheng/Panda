import { Directive, Input, TemplateRef, ViewContainerRef } from '@angular/core';
import { PermissionService } from '../services/permission.service';

@Directive({
  selector: '[appAuth]'
})
export class appAuthDirective {

  constructor(
    private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef,
    private permission: PermissionService
  ) {

    permission.stateChange().subscribe(res => {
      this.updateStatus()
    })
  }

  hasView = false;

  $name = "";

  @Input("appAuth") set appAuth(name: string) {
    console.log('status:' + status)
    this.$name = name;
    this.updateStatus();
  }

  updateStatus() {
    if (this.permission.checkPermission(this.$name)) {
      this.viewContainer.createEmbeddedView(this.templateRef);
      this.hasView = true;
    } else {
      this.viewContainer.clear();
      this.hasView = false;
    }
  }

}
