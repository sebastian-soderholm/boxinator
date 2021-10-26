import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GuestShipmentPage } from './guest-shipment.page';

describe('GuestShipmentPage', () => {
  let component: GuestShipmentPage;
  let fixture: ComponentFixture<GuestShipmentPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GuestShipmentPage ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GuestShipmentPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
