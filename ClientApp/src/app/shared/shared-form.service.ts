import { Injectable } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";

@Injectable({
	providedIn: 'root'
})

export class SharedFormService {
	//email = "uujee@fi.fi";

	constructor(private readonly fb: FormBuilder){}

	sharedForm(): FormGroup {
		const fg = this.fb.group({
			senderEmail: ['', [Validators.required, Validators.pattern(/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/)]],
			receiverFirstName: ['', [Validators.required, Validators.pattern(/[a-z]/gi)]],
			receiverLastName: ['', [Validators.required, Validators.pattern(/[a-z]/gi)]],
			destinationCountryId: [1, []],
			destinationZipCode: ['', Validators.required, Validators.minLength(5), Validators.pattern(/^[0-9]*$/)],
			destinationAddress: ['', Validators.required]
		})

		//fg.get('senderEmail')?.setValue(this.email);
		//fg.get('receiverFirstName')?.setValue("testipetteri");
		//fg.get('receiverLastName')?.setValue("testipetteri2");
		//fg.get('destinationCountryId')?.setValue(666);
		//fg.get('destinationZipCode')?.setValue(12312412);
		//fg.get('destinationAddress')?.setValue("uujeekatu 4");

		return fg;
	}
}