import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewShipmentPage } from './new-shipment.page';

describe('NewShipmentPage', () => {
  let component: NewShipmentPage;
  let fixture: ComponentFixture<NewShipmentPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewShipmentPage ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewShipmentPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
