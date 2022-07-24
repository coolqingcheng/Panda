import { Directive, Input, TemplateRef, ViewContainerRef } from '@angular/core';
import { AuthService } from '../services/auth.service'

@Directive({
  selector: '[appAuth]'
})
export class appAuthDirective {

  constructor(
    private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef,
    private permission: AuthService
  ) {

    permission.stateChange().subscribe(res => {
      this.updateStatus()
    })
  }


  $name = "";

  @Input("appAuth") set appAuth(name: string) {
    // console.log('status:' + status)
    this.$name = name;
    this.updateStatus();
  }

  updateStatus() {
    if (this.permission.checkPermission(this.$name)) {
      this.viewContainer.clear();
      this.viewContainer.createEmbeddedView(this.templateRef);
    } else {
      this.viewContainer.clear();
    }
  }

}
