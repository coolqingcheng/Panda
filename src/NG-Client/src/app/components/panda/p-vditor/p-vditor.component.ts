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

  vditor?: Vditor;

  isInit = false;

  @ViewChild('vditor') viewelement!: ElementRef;

  private onChange = (_: any) => { };
  private onTouched = (_: any) => { };


  private md = ''

  constructor() { }

  writeValue(obj: any): void {
    console.log('OBJ:', obj)
    if (obj) {
      this.md = obj
    }
    if (this.isInit) {
      this.vditor?.setValue(obj)
    }
  }
  registerOnChange(fn: any): void {
    if (fn) {
      this.onChange = fn;
    }
  }
  registerOnTouched(fn: any): void {
    if (fn) {
      this.onTouched = fn;
    }
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
        this.isInit = true
        this.vditor?.setValue(this.md);
      },
      blur: (v: string) => {
        this.onChange(v)
      },
      upload: {
        url: '/admin/upload',
        linkToImgUrl: '/admin/upload',
        format: (files, text) => {
          console.log('text:', text)
          let obj = JSON.parse(text)
          let file = obj.url.match(new RegExp(/\w+\.\w+/));
          var resp = {
            msg: obj.message,
            code: obj.code,
            data: {
              errFiles: [],
              succMap: {
                file: obj.url
              }
            }
          }
          return JSON.stringify(resp)
        }
      }
    });
  }

  ngAfterViewInit() {
    this.init();
  }

}
