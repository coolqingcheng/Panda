import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'p-form-submit-box',
  templateUrl: './p-form-submit-box.component.html',
  styleUrls: ['./p-form-submit-box.component.less']
})
export class PFormSubmitBoxComponent implements OnInit {

  constructor() { }

  @Input() left = false

  ngOnInit(): void {
  }

}
