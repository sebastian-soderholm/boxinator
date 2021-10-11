import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CountrySettingsPage } from './country-settings.page';

describe('CountrySettingsPage', () => {
  let component: CountrySettingsPage;
  let fixture: ComponentFixture<CountrySettingsPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CountrySettingsPage ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CountrySettingsPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
