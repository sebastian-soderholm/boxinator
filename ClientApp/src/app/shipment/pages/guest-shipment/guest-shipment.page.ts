import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Country } from 'src/app/login/models/country.model';
import { CountryService } from 'src/app/login/services/country.service';
import { SessionService } from 'src/app/login/services/session.service';
import { Box, BoxTypes } from '../../models/box.model';
import { GuestShipment } from '../../models/guest-shipment.model';
import { ColorPickerComponent } from 'ngx-color-picker';

@Component({
  selector: 'app-guest-shipment',
  templateUrl: './guest-shipment.page.html',
  styleUrls: ['./guest-shipment.page.scss']
})
export class GuestShipmentPage implements OnInit {

  private _guestShipment: GuestShipment = {
    email: "",
    firstName: "",
    lastName: "",
    countryId: 1,
    zipCode: "",
    address: "",
    cost: 0,
    boxes: []
  }

  private _countries: Country[] = []
  private _cost: number = 0;
  private _guestShipmentForm: any;
  private _boxFormArray: any;

  constructor(
    private readonly router: Router,
    private readonly countryService: CountryService,
    private readonly sessionService: SessionService
  ) { }

  ngOnInit(): void {
    this.countryService.fetchCountriesToSession(async () => {
      this._countries = this.sessionService.countries!;
      // console.table(this._countries)
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
      boxFormArray: this._boxFormArray = new FormArray([
        new FormGroup({
          boxType: new FormControl(this.boxTypes[0], []),
          boxColor: new FormControl({color: "rgb(255,255,255)"},[]),
        })
      ], Validators.required)
    });

   //Setup eventlisteners for form formchanges
    this._guestShipmentForm.get("destinationCountryId").valueChanges.subscribe((id:any) => {
      this.calculateCost()
   })
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
    const boxFormArray = this._guestShipmentForm.get('boxFormArray') as FormArray;

    // boxFormArray.controls.reduce((control) => this._guestShipment.cost += control.value.boxWeight.weight * selectedCountryMultiplier)
    console.table(boxFormArray)

    //Add field values to shipment
    this._guestShipment.email = this._guestShipmentForm.get('senderEmail')?.value
    this._guestShipment.firstName = this._guestShipmentForm.get('receiverFirstName')?.value
    this._guestShipment.lastName = this._guestShipmentForm.get('receiverLastName')?.value
    this._guestShipment.countryId = this._guestShipmentForm.get('destinationCountryId')?.value
    this._guestShipment.zipCode = this._guestShipmentForm.get('destinationZipCode')?.value

    //Add boxes to shipment
    for(let i=0; i<boxFormArray.length; i++) {
      const box: Box = {
        name: boxFormArray.at(i).get('boxType')?.value.name,
        weight: boxFormArray.at(i).get('boxType')?.value.weight,
        color: boxFormArray.at(i).get('boxColor')?.value.color
      }
      this._guestShipment.boxes.push(box)
    }

    console.log("Shipment data: ")
    console.table(this._guestShipment)

  }
  clearFormData() {
    this._guestShipment = {
      email: "",
      firstName: "",
      lastName: "",
      countryId: 1,
      zipCode: "",
      address: "",
      cost: 0,
      boxes: []
    }
  }

  formChanged() {
    // this.calculateCost()
   }
  calculateCost() {
    const countryMultiplier = this._guestShipmentForm.get('destinationCountryId').value
    const boxFormArray = this._guestShipmentForm.get('boxFormArray').controls;
    const boxesForShipment: Box[] = []

    // boxesForShipment.reduce()
    // // Calculate shipping cost
    // this._cost = boxesForShipment.reduce(function(cost: number, _curr: Box) {
    //   cost = box.weight * this._guestShipmentForm.get('destinationCountryId').value
    // });
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
    return this.guestShipmentForm.get('boxFormArray') as FormArray
  }
  get boxTypes() {
    return BoxTypes;
  }
  // get colorPicker() {
  //   return this._ColorPickerComponent;
  // }
  // changeColor(event: any){
  //   console.log("Color changed: " + JSON.stringify(this._boxFormGroup.get('boxColor')))
  // }

  colorChanged(){
    // console.log("Color changed: " + this.color)
  }
  //All available countries
  get countries() {
    return this._countries;
  }

}
