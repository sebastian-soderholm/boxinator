import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditShipmentPage } from './edit-shipment.page';

describe('EditShipmentComponent', () => {
  let component: EditShipmentPage;
  let fixture: ComponentFixture<EditShipmentPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditShipmentPage ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditShipmentPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
