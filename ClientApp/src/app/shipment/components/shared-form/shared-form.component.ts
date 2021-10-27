import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { SharedShipmentFormService } from 'src/app/shipment/services/shared-form.service';
import { FormGroup, FormBuilder } from "@angular/forms";
import { Country } from 'src/app/login/models/country.model';
import { CountryService } from 'src/app/login/services/country.service';
import { SessionService } from 'src/app/shared/session.service';

@Component({
  selector: 'app-shared-form',
  templateUrl: './shared-form.component.html',
  styleUrls: ['./shared-form.component.scss']
})
export class SharedFormComponent implements OnInit {
  @Input() parentGroup: any;
  @Input() header: any;
  countries: Country[] | undefined;

  constructor(
    private fb: FormBuilder,
    private sharedShipmentFormService: SharedShipmentFormService,
    private readonly countryService: CountryService,
    private readonly sessionService: SessionService,


    ) { 
      this.countryService.fetchCountriesToSession(() =>{
        this.countries = this.sessionService!.countries;
      })
  }

  ngOnInit(): void {
  }


}
