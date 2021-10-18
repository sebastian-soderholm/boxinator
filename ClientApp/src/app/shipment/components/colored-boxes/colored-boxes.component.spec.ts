import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ColoredBoxesComponent } from './colored-boxes.component';

describe('ColoredBoxesComponent', () => {
  let component: ColoredBoxesComponent;
  let fixture: ComponentFixture<ColoredBoxesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ColoredBoxesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ColoredBoxesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
