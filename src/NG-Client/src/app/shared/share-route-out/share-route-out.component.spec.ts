import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShareRouteOutComponent } from './share-route-out.component';

describe('ShareRouteOutComponent', () => {
  let component: ShareRouteOutComponent;
  let fixture: ComponentFixture<ShareRouteOutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShareRouteOutComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShareRouteOutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
