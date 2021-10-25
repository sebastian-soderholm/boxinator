import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SessionService } from 'src/app/shared/session.service';
import { SharedShipmentFormService } from 'src/app/shipment/services/shared-form.service';
import { FormGroup, FormBuilder, NgForm } from "@angular/forms";
import { ShipmentTableData } from '../../models/shipment-table.model';
import { ShipmentService } from '../../services/shipment.service';
import { CreateShipment } from '../../models/create-shipment.model';
import { Observable } from 'rxjs';
import { EditShipment } from '../../models/edit-shipment.model';
import { Country } from 'src/app/login/models/country.model';

@Component({
  selector: 'app-edit-shipment',
  templateUrl: './edit-shipment.page.html',
  styleUrls: ['./edit-shipment.page.scss']
})
export class EditShipmentPage implements OnInit, OnChanges {
  shipmentId: number;
  editForm: FormGroup;
  fetchedShipment: EditShipment | undefined;

  constructor(
    private readonly actRoute: ActivatedRoute,
    private readonly sessionService: SessionService,
    private fb: FormBuilder,
    private sharedShipmentFormService: SharedShipmentFormService,
    private readonly shipmentService: ShipmentService
    ) {   
      this.editForm = this.fb.group({
        header: this.sharedShipmentFormService.sharedForm(null),
        //additionalField: [null]
      })

      this.shipmentId = this.actRoute.snapshot.params.id;

      this.shipmentService.getByIdObservable(Number(this.shipmentId!))
      .subscribe((result : EditShipment) => {
        console.log(result)
        this.editForm = this.fb.group({
          header: this.sharedShipmentFormService.sharedForm(result),
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
    this.shipmentService.updateShipment(this.shipmentId, this.editForm!.value.header, async ()=> {
      console.log("updated");
    })
  }

}
