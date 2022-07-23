import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountLogsComponent } from './account-logs.component';

describe('AccountLogsComponent', () => {
  let component: AccountLogsComponent;
  let fixture: ComponentFixture<AccountLogsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AccountLogsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AccountLogsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
