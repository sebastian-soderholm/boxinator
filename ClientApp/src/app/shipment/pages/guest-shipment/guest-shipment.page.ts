import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Country } from 'src/app/login/models/country.model';
import { CountryService } from 'src/app/login/services/country.service';
import { SessionService } from 'src/app/login/services/session.service';
import { BasicBox, Box, DeluxeBox, HumbleBox, PremiumBox } from '../../models/box.model';
import { GuestShipment } from '../../models/guest-shipment.model';

@Component({
  selector: 'app-guest-shipment',
  templateUrl: './guest-shipment.page.html',
  styleUrls: ['./guest-shipment.page.scss']
})
export class GuestShipmentPage implements OnInit {

  private _guestShipment: GuestShipment = {
    senderEmail: "",
    receiverFirstName: "",
    receiverLastName: "",
    destinationCountryId: 0,
    destinationZipCode: "",
    cost: 0
  }
  private _box: Box = {
    name: "Basic",
    weight: 1,
    colorR: 0,
    colorG: 0,
    colorB: 0
  }
  private _countries: Country[] = []
  private _guestShipmentForm: any;
  private _boxesForShipment: Box[] = [];
  private _boxesForm = new FormArray([]);
  private _boxForm: any;
  private _cost: number = 0;

  constructor(
    private readonly router: Router,
    private readonly countryService: CountryService,
    private readonly sessionService: SessionService
  ) { }

  ngOnInit(): void {
    this.countryService.fetchCountriesToSession(async () => {
      this._countries = this.sessionService.countries!;
    });

    this._guestShipmentForm = new FormGroup({
      senderEmail: new FormControl(this._guestShipment.senderEmail, [
        Validators.required,
        //Must be in email format
        Validators.pattern(/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/),
      ]),
      receiverFirstName: new FormControl(this._guestShipment.receiverFirstName, [
        Validators.required,
        //Must contain letters
        Validators.pattern(/[a-z]/gi)
      ]),
      receiverLastName: new FormControl(this._guestShipment.receiverLastName, [
        Validators.required,
        //Must contain letters
        Validators.pattern(/[a-z]/gi)
      ]),
      destinationCountryId: new FormControl(this._guestShipment.destinationCountryId, [
      ]),
      destinationZipCode: new FormControl(this._guestShipment.destinationZipCode, [
        Validators.required,
        //Must be a minimum length
        Validators.minLength(5),
        //Must contain only numbers
        Validators.pattern(/^[0-9]*$/)
      ]),
    });

    //Form for adding a single box to shipment
    this._boxForm = new FormGroup({
      boxWeight: new FormControl(this._box.weight, [])
    });
  }

  calculateCost() {
    // this._cost = this._boxesForShipment.reduce((cost: number, box: Box) => {
    //   cost = box.weight * this._guestShipmentForm.get('destinationCountryId').value
    // });
  }

  addBox() {
    console.log("Adding new boxform...")
    this._boxesForm.push(new FormControl(this._boxForm))
  }
  removeBox(index: number) {
    this._boxesForm.removeAt(index)
  }

  createGuestShipment(): void {
    this._guestShipment.receiverFirstName = this._guestShipmentForm.get('receiverFirstName').value
    this._guestShipment.receiverLastName = this._guestShipmentForm.get('receiverLastName').value
    this._guestShipment.senderEmail = this._guestShipmentForm.get('senderEmail').value

    //Apply countryId only if selected
    this._guestShipment.destinationCountryId = this._guestShipmentForm.get('destinationCountryId').value

    this._guestShipment.destinationZipCode = this._guestShipmentForm.get('destinationZipCode').value

    console.log("Shipment info: " + JSON.stringify(this._guestShipment))
    // this.registerService.registerUser(this._registerUser, function(){
    //   console.log("User registered successfully!")
    // })
  }
  get guestShipmentForm() {
    return this._guestShipmentForm
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
  get cost() {
    return this._cost
  }
  //Box form
  get boxForm() {
    return this._boxForm
  }
  get boxesForm() {
    return this._boxesForm
  }
  get boxesForShipment() {
    return this._boxesForShipment;
  }
  //All available countries
  get countries() {
    return this._countries;
  }

}
