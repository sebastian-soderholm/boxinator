import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { SharedShipmentFormService } from 'src/app/shipment/services/shared-form.service';
import { FormGroup, FormBuilder } from "@angular/forms";

@Component({
  selector: 'app-shared-form',
  templateUrl: './shared-form.component.html',
  styleUrls: ['./shared-form.component.scss']
})
export class SharedFormComponent implements OnInit {
  @Input() parentGroup: any;
  @Input() header: any;

  constructor(
    private fb: FormBuilder,
    private sharedShipmentFormService: SharedShipmentFormService
    ) { 
  }

  ngOnInit(): void {
  }


}
