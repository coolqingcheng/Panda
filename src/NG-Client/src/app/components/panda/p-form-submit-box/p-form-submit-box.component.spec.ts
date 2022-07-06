import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PFormSubmitBoxComponent } from './p-form-submit-box.component';

describe('PFormSubmitBoxComponent', () => {
  let component: PFormSubmitBoxComponent;
  let fixture: ComponentFixture<PFormSubmitBoxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PFormSubmitBoxComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PFormSubmitBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
