import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyShipmentsPage } from './my-shipments.page';

describe('MyShipmentsPage', () => {
  let component: MyShipmentsPage;
  let fixture: ComponentFixture<MyShipmentsPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MyShipmentsPage ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MyShipmentsPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
