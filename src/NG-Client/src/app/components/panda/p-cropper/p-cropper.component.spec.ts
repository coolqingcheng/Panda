import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PCropperComponent } from './p-cropper.component';

describe('PCropperComponent', () => {
  let component: PCropperComponent;
  let fixture: ComponentFixture<PCropperComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PCropperComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PCropperComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
