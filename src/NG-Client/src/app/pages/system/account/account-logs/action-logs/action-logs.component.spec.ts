import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActionLogsComponent } from './action-logs.component';

describe('ActionLogsComponent', () => {
  let component: ActionLogsComponent;
  let fixture: ComponentFixture<ActionLogsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ActionLogsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ActionLogsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
