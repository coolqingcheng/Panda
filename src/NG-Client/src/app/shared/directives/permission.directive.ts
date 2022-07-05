import { Directive, Input, TemplateRef, ViewContainerRef } from '@angular/core';

@Directive({
  selector: '[permission]'
})
export class PermissionDirective {

  constructor(
    private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef
  ) { }

  hasView = false;

  @Input() set permission(status: boolean) {
    if (status) {
      this.viewContainer.createEmbeddedView(this.templateRef);
      this.hasView = true;
    } else {
      this.viewContainer.clear();
      this.hasView = false;
    }
  }

}
