import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FriendlinkEditComponent } from './friendlink-edit.component';

describe('FriendlinkEditComponent', () => {
  let component: FriendlinkEditComponent;
  let fixture: ComponentFixture<FriendlinkEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FriendlinkEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FriendlinkEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
