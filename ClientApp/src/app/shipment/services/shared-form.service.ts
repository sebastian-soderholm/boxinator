import { Injectable, Input } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { CreateShipment } from "../models/create-shipment.model";
import { ShipmentTableData } from "../models/shipment-table.model";
import { ShipmentService } from "./shipment.service";
import { EditShipment } from "../models/edit-shipment.model";

@Injectable({
	providedIn: 'root'
})

export class SharedShipmentFormService {
	validStrPattern: string = '[a-zA-ZÆæØøßÅÄÖåäö]*';
	validAddressPattern: string = '^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _][a-zA-ZÆæØøßÅÄÖåäö]*$';

	constructor(private readonly fb: FormBuilder, private readonly shipmentService: ShipmentService){
	}

	sharedForm(shipment: EditShipment | null): FormGroup {
		const fg = this.baseFormValidations();

		if(shipment != null) {
			console.log(shipment)
			fg.get('senderEmail')?.setValue(shipment.sender.email);
			fg.get('receiverFirstName')?.setValue(shipment.receiverFirstName);
			fg.get('receiverLastName')?.setValue(shipment.receiverLastName);
			fg.get('destinationCountryId')?.setValue(shipment.country.id);
			fg.get('destinationZipCode')?.setValue(shipment.receiverZipCode.toString());
			fg.get('destinationAddress')?.setValue(shipment.receiverAddress);
		}

		return fg;
	}

	baseFormValidations() {
		return this.fb.group({
			senderEmail: ['', [Validators.required, Validators.pattern(/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/)]],
			receiverFirstName: ['', [Validators.required, Validators.pattern(this.validStrPattern)]],
			receiverLastName: ['', [Validators.required, Validators.pattern(this.validStrPattern)]],
			destinationCountryId: [1, [Validators.required, Validators.min(1)]],
			destinationZipCode: ['', [Validators.required, Validators.minLength(5), Validators.pattern(/^[0-9]*$/)]],
			destinationAddress: ['', [Validators.required, Validators.pattern(this.validAddressPattern)]]
		})
	}

}