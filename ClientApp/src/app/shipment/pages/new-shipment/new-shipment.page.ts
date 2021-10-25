import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Country } from 'src/app/login/models/country.model';
import { CountryService } from 'src/app/login/services/country.service';
import { SessionService } from 'src/app/shared/session.service';
import { Box, BoxTypes } from '../../../shared/box.model';
import { ShipmentService } from '../../services/shipment.service';
import { CreateShipment } from '../../models/create-shipment.model';
import { LoginService } from 'src/app/login/services/login.service';

@Component({
  selector: 'app-new-shipment',
  templateUrl: './new-shipment.page.html',
  styleUrls: ['./new-shipment.page.scss']
})
export class NewShipmentPage implements OnInit {
  private _newShipment: CreateShipment = {
    senderId: undefined,
    receiverFirstName: "",
    receiverLastName: "",
    receiverZipCode: "",
    receiverAddress: "",
    countryId: 1,
    cost: 0,
    boxes: []
  }

  private _countries: Country[] = []
  private _boxes: Box[] = []
  private _boxFormIsEmpty: boolean = false;
  private _cost: number = 200;
  private _newShipmentForm: any;
  private _boxFormArray: any;

  constructor(
    private readonly router: Router,
    private readonly countryService: CountryService,
    private readonly sessionService: SessionService,
    private readonly shipmentService: ShipmentService,
    private readonly loginService: LoginService
  ) { }

  ngOnInit(): void {
    this.countryService.fetchCountriesToSession(async () => {
      this._countries = this.sessionService.countries!;
    });

    this._newShipmentForm = new FormGroup({
      senderEmail: new FormControl('', [
        Validators.required,
        //Must be in email format
        Validators.pattern("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$"),
      ]),
      receiverFirstName: new FormControl('', [
        Validators.required,
        //Must contain letters
        Validators.pattern("[a-zA-ZÆæØøßÅÄÖåäö]*")
      ]),
      receiverLastName: new FormControl('', [
        Validators.required,
        //Must contain letters
        Validators.pattern("[a-zA-ZÆæØøßÅÄÖåäö]*")
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
    this._newShipmentForm.get("destinationCountryId").valueChanges.subscribe((id:any) => {
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
    const boxFormArray = this._newShipmentForm.get('boxFormArray') as FormArray;
    boxFormArray.push(group)
  }
  removeBox(index: number) {
    this._boxFormArray.removeAt(index)
  }
  clearAllBoxes(){
    this._boxFormArray.clear()
  }

  createNewShipment(): void {
    this.getFormData()
    this.clearFormData();
  }
  get newShipmentForm() {
    return this._newShipmentForm
  }

  getFormData() {
    this.calculateCost()
    this._newShipment.cost = this._cost;

    //Add field values to shipment
    this._newShipment.receiverFirstName = this._newShipmentForm.get('receiverFirstName')?.value
    this._newShipment.receiverLastName = this._newShipmentForm.get('receiverLastName')?.value
    this._newShipment.countryId = this._newShipmentForm.get('destinationCountryId')?.value
    this._newShipment.receiverAddress = this._newShipmentForm.get('destinationAddress')?.value
    this._newShipment.receiverZipCode = this._newShipmentForm.get('destinationZipCode')?.value

    //Add boxes to shipment
    this._newShipment.boxes = this._boxes

    //Set logged in user id for shipment
    this._newShipment.senderId = this.loginService.user?.id;

    //Add cost to shipment
    this.calculateCost()
    this._newShipment.cost = this.cost

    //Post shipment
    this.shipmentService.postNewShipment(<CreateShipment>this._newShipment, () => console.log("hurray!"));

  }
  clearFormData() {
    this._newShipment = {
      senderId: undefined,
      receiverFirstName: "",
      receiverLastName: "",
      receiverZipCode: "",
      receiverAddress: "",
      countryId: 1,
      cost: 0,
      boxes: []
    }
  }
  calculateCost() {
    //Get weights of all boxes
    const boxWeightArray = this._boxes.map((box: Box) => box.weight);

    //Get selected country object
    const selectedCountry = this._countries.find((country: Country) => {
      return country.id == this._newShipmentForm.get('destinationCountryId').value
    })

    this._cost = 200;
    // Calculate shipping cost if any boxes present
    if(boxWeightArray.length > 0) {
      boxWeightArray.forEach((weight: number) => {
        this._cost += weight * selectedCountry!.countryMultiplier
      });
    }
  }

  //Form fields
  get senderEmail() {
    return this._newShipmentForm.get('senderEmail')
  }
  get receiverFirstName() {
    return this._newShipmentForm.get('receiverFirstName')
  }
  get receiverLastName() {
    return this._newShipmentForm.get('receiverLastName')
  }
  get destinationCountryId() {
    return this._newShipmentForm.get('destinationCountryId')
  }
  get destinationZipCode() {
    return this._newShipmentForm.get('destinationZipCode')
  }
  get destinationAddress() {
    return this._newShipmentForm.get('destiantionAddress')
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
