import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SessionService } from 'src/app/shared/session.service';
import { SharedFormService } from 'src/app/shared/shared-form.service';
import { FormGroup, FormBuilder, NgForm } from "@angular/forms";

@Component({
  selector: 'app-edit-shipment',
  templateUrl: './edit-shipment.page.html',
  styleUrls: ['./edit-shipment.page.scss']
})
export class EditShipmentPage implements OnInit {
  shipmentId: number | undefined;
  editForm: FormGroup;

  constructor(
    private readonly actRoute: ActivatedRoute,
    private readonly sessionService: SessionService,
    private fb: FormBuilder,
    private sharedFormService: SharedFormService
    ) {     
      this.editForm = this.fb.group({
        header: this.sharedFormService.sharedForm(),
        additionalField: [null],
        additionalField2: [null]
      })

      this.shipmentId = this.actRoute.snapshot.params.id;
      let shipment = this.sessionService!.shipmentTableData!.find(l => l.id == this.shipmentId);
      console.log(shipment)
  }

  ngOnInit(): void {
  }

  onSubmitEditForm(){
    console.log(this.editForm.value.header)
    console.log(this.editForm.value.additionalField)
    console.log("helloop");
  }


}
