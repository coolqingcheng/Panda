import { Directive, Input, TemplateRef, ViewContainerRef } from '@angular/core';

@Directive({
  selector: '[appAuth]'
})
export class appAuthDirective {

  constructor(
    private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef
  ) { }

  hasView = false;

  @Input("appAuth") set appAuth(status: boolean) {
    console.log('status:' + status)
    if (status) {
      this.viewContainer.createEmbeddedView(this.templateRef);
      this.hasView = true;
    } else {
      this.viewContainer.clear();
      this.hasView = false;
    }
  }

}
