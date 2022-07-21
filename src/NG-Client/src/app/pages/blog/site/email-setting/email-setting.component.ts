import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-email-setting',
  templateUrl: './email-setting.component.html',
  styleUrls: ['./email-setting.component.less']
})
export class EmailSettingComponent implements OnInit {

  constructor(
    private fb:FormBuilder
  ) { 
    this.formGroup = this.fb.group({

    })
  }

  loading = false

  formGroup!:FormGroup;

  ngOnInit(): void {
  }

}
