import { Injectable, Input } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { CreateShipment } from "../shipment/models/create-shipment.model";
import { ShipmentTableData } from "../shipment/models/shipment-table.model";
import { ShipmentService } from "../shipment/services/shipment.service";

@Injectable({
	providedIn: 'root'
})

export class SharedFormService {

	constructor(private readonly fb: FormBuilder, private readonly shipmentService: ShipmentService){

	}

	sharedForm(shipment: CreateShipment | null): FormGroup {
		const fg = this.fb.group({
			senderEmail: ['', [Validators.required, Validators.pattern(/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/)]],
			receiverFirstName: ['', [Validators.required, Validators.pattern(/[a-z]/gi)]],
			receiverLastName: ['', [Validators.required, Validators.pattern(/[a-z]/gi)]],
			destinationCountryId: [1, [Validators.required, Validators.min(1)]],
			destinationZipCode: ['', Validators.required, Validators.minLength(5), Validators.pattern(/^[0-9]*$/)],
			destinationAddress: ['', Validators.required]
		})

		if(shipment != null) {
			console.log(shipment)
			fg.get('senderEmail')?.setValue("emptyemail@test.com");
			fg.get('receiverFirstName')?.setValue(shipment.receiverFirstName);
			fg.get('receiverLastName')?.setValue(shipment.receiverLastName);
			fg.get('destinationCountryId')?.setValue(shipment.countryId);
			//fg.get('destinationZipCode')?.setValue(shipment.receiverZipCode.toString());
			fg.get('destinationAddress')?.setValue(shipment.receiverAddress);

			//fg.get('destinationCountryId')?.setValue(shipment.countryId);
			//fg.get('destinationZipCode')?.setValue(shipment.receiverZipCode);
			//fg.get('destinationAddress')?.setValue(shipment.receiverAddress);
		}

		return fg;
	}

}