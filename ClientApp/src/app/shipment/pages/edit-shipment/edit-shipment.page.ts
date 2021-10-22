import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SessionService } from 'src/app/shared/session.service';
import { SharedFormService } from 'src/app/shared/shared-form.service';
import { FormGroup, FormBuilder, NgForm } from "@angular/forms";
import { ShipmentTableData } from '../../models/shipment-table.model';
import { ShipmentService } from '../../services/shipment.service';
import { CreateShipment } from '../../models/create-shipment.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-edit-shipment',
  templateUrl: './edit-shipment.page.html',
  styleUrls: ['./edit-shipment.page.scss']
})
export class EditShipmentPage implements OnInit, OnChanges {
  shipmentId: number | undefined;
  editForm: FormGroup | null;
  fetchedShipment: CreateShipment | undefined;

  constructor(
    private readonly actRoute: ActivatedRoute,
    private readonly sessionService: SessionService,
    private fb: FormBuilder,
    private sharedFormService: SharedFormService,
    private readonly shipmentService: ShipmentService
    ) {   
      this.editForm = this.fb.group({
        header: this.sharedFormService.sharedForm(null),
        //additionalField: [null]
      })

      this.shipmentId = this.actRoute.snapshot.params.id;
      this.shipmentService.getByIdObservable(Number(this.shipmentId!))
      .subscribe((result : CreateShipment) => {
        this.editForm = this.fb.group({
          header: this.sharedFormService.sharedForm(result),
          //additionalField: [null]
        })
      });

  }

  ngOnInit(): void {
  }

  ngOnChanges() {
  }

  onSubmitEditForm(){
    console.log(this.editForm!.value.header)
    //console.log(this.editForm.value.additionalField)
    console.log("helloop");
  }

}
