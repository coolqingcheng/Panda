import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FriendlinkListComponent } from './friendlink-list.component';

describe('FriendlinkListComponent', () => {
  let component: FriendlinkListComponent;
  let fixture: ComponentFixture<FriendlinkListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FriendlinkListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FriendlinkListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
