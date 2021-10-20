import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Country } from 'src/app/login/models/country.model';
import { CountryService } from 'src/app/login/services/country.service';
import { SessionService } from 'src/app/shared/session.service';
import { Box, BoxTypes } from '../../../shared/box.model';
import { GuestShipment } from '../../models/guest-shipment.model';
import { ColorPickerComponent } from 'ngx-color-picker';
import { ShipmentService } from '../../services/shipment.service';
import { BoxFormComponent } from '../../components/box-form/box-form.component';


@Component({
  selector: 'app-guest-shipment',
  templateUrl: './guest-shipment.page.html',
  styleUrls: ['./guest-shipment.page.scss']
})
export class GuestShipmentPage implements OnInit {

  private _guestShipment: GuestShipment = {
    email: "",
    receiverFirstName: "",
    receiverLastName: "",
    receiverZipCode: "",
    receiverAddress: "",
    cost: 0,
    countryId: 1,
    boxes: []
  }

  private _countries: Country[] = []
  private _boxes: Box[] = []
  private _boxFormIsEmpty: boolean = false;
  private _cost: number = 0;
  private _guestShipmentForm: any;
  private _boxFormArray: any;

  constructor(
    private readonly router: Router,
    private readonly countryService: CountryService,
    private readonly sessionService: SessionService,
    private readonly shipmentService: ShipmentService
  ) { }

  ngOnInit(): void {
    this.countryService.fetchCountriesToSession(async () => {
      this._countries = this.sessionService.countries!;
    });

    this._guestShipmentForm = new FormGroup({
      senderEmail: new FormControl('', [
        Validators.required,
        //Must be in email format
        Validators.pattern(/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/),
      ]),
      receiverFirstName: new FormControl('', [
        Validators.required,
        //Must contain letters
        Validators.pattern(/[a-z]/gi)
      ]),
      receiverLastName: new FormControl('', [
        Validators.required,
        //Must contain letters
        Validators.pattern(/[a-z]/gi)
      ]),
      destinationCountryId: new FormControl(1, [
      ]),
      destinationZipCode: new FormControl('', [
        Validators.required,
        //Must be a minimum length
        Validators.minLength(5),
        //Must contain only numbers
        Validators.pattern(/^[0-9]*$/)
      ]),
      destinationAddress: new FormControl('',[
        Validators.required
      ]),
    });

    this.onChanges();
  }
  onChanges(): void {
    //Setup eventlisteners for formchanges
    this._guestShipmentForm.get("destinationCountryId").valueChanges.subscribe((id:any) => {
      this.calculateCost()
   })
  }
  boxFormChanged(boxes: Box[]) {
    this._boxes = boxes;
    this._boxes.length === 0 ? this._boxFormIsEmpty = true : this._boxFormIsEmpty = false
    this.calculateCost()
  }
  addBox() {
    const group = new FormGroup({
      boxType: new FormControl(this.boxTypes[0]),
      boxColor: new FormControl({color: 'rgb(255,255,255)'})
    });
    const boxFormArray = this._guestShipmentForm.get('boxFormArray') as FormArray;
    boxFormArray.push(group)
  }
  removeBox(index: number) {
    // const boxFormArray = this._guestShipmentForm.get('boxFormArray') as FormArray;
    this._boxFormArray.removeAt(index)
  }
  clearAllBoxes(){
    this._boxFormArray.clear()
  }

  createGuestShipment(): void {
    this.getFormData()
    this.clearFormData();
  }
  get guestShipmentForm() {
    return this._guestShipmentForm
  }

  getFormData() {

    //Add field values to shipment
    this._guestShipment.receiverFirstName = this._guestShipmentForm.get('receiverFirstName')?.value
    this._guestShipment.receiverLastName = this._guestShipmentForm.get('receiverLastName')?.value
    this._guestShipment.countryId = this._guestShipmentForm.get('destinationCountryId')?.value
    this._guestShipment.receiverAddress = this._guestShipmentForm.get('destinationAddress')?.value
    this._guestShipment.receiverZipCode = this._guestShipmentForm.get('destinationZipCode')?.value

    //Add cost to shipment
    this.calculateCost()
    this._guestShipment.cost = this.cost

    //Add boxes to shipment
    this._guestShipment.boxes = this._boxes

    //Post shipment
    console.table(this._guestShipment)
    this.shipmentService.postNewGuestShipment(<GuestShipment>this._guestShipment, () => console.log("hurray!"));

  }
  clearFormData() {
    this._guestShipment = {
      email: "",
      receiverFirstName: "",
      receiverLastName: "",
      countryId: 1,
      receiverZipCode: "",
      receiverAddress: "",
      cost: 0,
      boxes: []
    }
  }
  calculateCost() {
    //Get weights of all boxes
    const boxWeightArray = this._boxes.map((box: Box) => box.weight);

    //Get selected country object
    const selectedCountry = this._countries.find((country: Country) => {
      return country.id == this._guestShipmentForm.get('destinationCountryId').value
    })

    this._cost = 0;
    // Calculate shipping cost if any boxes present
    if(boxWeightArray.length > 0) {
      boxWeightArray.forEach((weight: number) => {
        this._cost += weight * selectedCountry!.countryMultiplier
      });
    }
  }

  //Form fields
  get senderEmail() {
    return this._guestShipmentForm.get('senderEmail')
  }
  get receiverFirstName() {
    return this._guestShipmentForm.get('receiverFirstName')
  }
  get receiverLastName() {
    return this._guestShipmentForm.get('receiverLastName')
  }
  get destinationCountryId() {
    return this._guestShipmentForm.get('destinationCountryId')
  }
  get destinationZipCode() {
    return this._guestShipmentForm.get('destinationZipCode')
  }
  get destinationAddress() {
    return this._guestShipmentForm.get('destiantionAddress')
  }
  get cost() {
    return this._cost
  }
  get boxFormArray() {
    return this._boxFormArray.get('boxFormArray') as FormArray
  }
  get boxTypes() {
    return BoxTypes;
  }
  get boxFormIsEmpty() {
    return this._boxFormIsEmpty
  }
  //All available countries
  get countries() {
    return this._countries;
  }

}
