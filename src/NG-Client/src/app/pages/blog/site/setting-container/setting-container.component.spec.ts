import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SettingContainerComponent } from './setting-container.component';

describe('SettingContainerComponent', () => {
  let component: SettingContainerComponent;
  let fixture: ComponentFixture<SettingContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SettingContainerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SettingContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
