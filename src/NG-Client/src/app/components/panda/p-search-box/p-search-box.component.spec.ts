import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PSearchBoxComponent } from './p-search-box.component';

describe('PSearchBoxComponent', () => {
  let component: PSearchBoxComponent;
  let fixture: ComponentFixture<PSearchBoxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PSearchBoxComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PSearchBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
