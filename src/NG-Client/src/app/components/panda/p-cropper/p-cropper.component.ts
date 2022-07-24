import { Component, ElementRef, Input, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import Cropper from 'cropperjs';
import { NzModalRef } from 'ng-zorro-antd/modal';

@Component({
  selector: 'app-p-cropper',
  templateUrl: './p-cropper.component.html',
  styleUrls: ['./p-cropper.component.less', './cropper.css'],
  encapsulation: ViewEncapsulation.None
})
export class PCropperComponent implements OnInit {

  constructor(
    private modal: NzModalRef
  ) { }

  ngOnInit(): void {
  }

  /**
   * 图片地址或者base64
   */
  @Input() imgData!:string

  @Input('ratio') ratio: number = 1 / 1;

  @Input() width!: number;

  @Input() height!: number;

  @ViewChild("img") img!: ElementRef;

  loading = true

  cropper?: Cropper;

  ngAfterViewInit() {
    console.log('初始化cropperjs  ' + this.img.nativeElement.src)
    let that = this
    this.cropper = new Cropper(this.img.nativeElement, {
      aspectRatio: this.ratio,
      viewMode: 1,
      ready() {
        that.loading = false
      }
    });
  }

  getImageData() {
    let base64 = this.cropper?.getCroppedCanvas({
      width: this.width, height: this.height
    }).toDataURL('png');
    this.modal.close({
      success: true,
      data: base64
    })
  }

}
