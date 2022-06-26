import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteSettingComponent } from './site-setting.component';

describe('SiteSettingComponent', () => {
  let component: SiteSettingComponent;
  let fixture: ComponentFixture<SiteSettingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SiteSettingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
