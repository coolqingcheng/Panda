import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-site-setting',
  templateUrl: './site-setting.component.html',
  styleUrls: ['./site-setting.component.less']
})
export class SiteSettingComponent implements OnInit {


  formGroup!:FormGroup;

  content = '# var a  = 100'

  options = {
    lineNumbers: true,
    readOnly: false, // nocursor can not copy
    mode: 'javascript',
    autofocus: true,
    lineWiseCopyCut: true,
    cursorBlinkRate: 500 // hide cursor
  };

  constructor(
    private fb:FormBuilder
  ) { 
    this.formGroup = this.fb.group({
      statistic:['',[Validators.required]]
    })
  }

  ngOnInit(): void {
  }

  saving = false;

}
