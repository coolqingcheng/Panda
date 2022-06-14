import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PVditorComponent } from './p-vditor.component';

describe('PVditorComponent', () => {
  let component: PVditorComponent;
  let fixture: ComponentFixture<PVditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PVditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PVditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
