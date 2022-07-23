import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SendTestEmailComponent } from './send-test-email.component';

describe('SendTestEmailComponent', () => {
  let component: SendTestEmailComponent;
  let fixture: ComponentFixture<SendTestEmailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SendTestEmailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SendTestEmailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
