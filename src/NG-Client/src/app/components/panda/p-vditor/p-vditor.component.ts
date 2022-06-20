import { Component, ElementRef, forwardRef, OnInit, ViewChild } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import Vditor from 'vditor';

@Component({
  selector: 'p-vditor',
  templateUrl: './p-vditor.component.html',
  styleUrls: [
    './p-vditor.component.less'
  ],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => PVditorComponent),
    multi: true
  }]
})
export class PVditorComponent implements OnInit, ControlValueAccessor {

  vditor!: Vditor;

  @ViewChild('vditor') viewelement!: ElementRef;

  private onChange = (_: any) => { };


  constructor() { }

  writeValue(obj: any): void {
    console.log('OBJ:', obj)
  }
  registerOnChange(fn: any): void {
    if (fn) {
      this.onChange = fn;
    }
  }
  registerOnTouched(fn: any): void {
  }

  ngOnInit(): void {
  }

  init() {
    this.vditor = new Vditor(this.viewelement.nativeElement, {
      height: 360,
      toolbarConfig: {
        pin: true,
      },
      cache: {
        enable: false,
      },
      mode: 'ir',
      icon: 'ant',
      outline: {
        enable: true,
        position: 'left'
      },
      after: () => {
        this.vditor.setValue('');
      }
    });
  }

  ngAfterViewInit() {
    console.log('native-ngAfterViewInit', this.viewelement)
    this.init();
  }

}
