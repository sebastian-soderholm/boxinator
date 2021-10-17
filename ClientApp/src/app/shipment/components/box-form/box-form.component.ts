import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormArray, FormGroup, FormControl, Validators } from '@angular/forms';
import { Box, BoxTypes } from '../../models/box.model';

@Component({
  selector: 'app-box-form',
  templateUrl: './box-form.component.html',
  styleUrls: ['./box-form.component.scss']
})
export class BoxFormComponent implements OnInit {
  @Output()
  formChange: EventEmitter<Box[]> = new EventEmitter();

  private _boxForm: FormGroup | any;
  private _boxFormArray: FormArray | any;

  constructor() {
    this._boxForm = new FormGroup({
      boxFormArray: this._boxFormArray = new FormArray([
        new FormGroup({
          boxType: new FormControl(this.boxTypes[0], []),
          boxColor: new FormControl({color: "rgb(255,255,255)"},[]),
        })
      ])
    })
  }

  ngOnInit(): void {
    this.onchange()
  }

  onchange() {
    this.boxFormArray.valueChanges.subscribe(event => {
      //Emit event
      this.formChange.emit(this.boxes)
    })

  }

  addBox() {
    const group = new FormGroup({
      boxType: new FormControl(this.boxTypes[0]),
      boxColor: new FormControl({color: 'rgb(255,255,255)'})
    });
    const boxFormArray = this._boxFormArray as FormArray;
    boxFormArray.push(group)
  }
  removeBox(index: number) {
    // const boxFormArray = this._guestShipmentForm.get('boxFormArray') as FormArray;
    this._boxFormArray.removeAt(index)
  }
  clearAllBoxes() {
    this._boxFormArray.clear()
  }
  colorChanged() {
    this.formChange.emit(this.boxes)
  }

  get boxTypes() {
    return BoxTypes;
  }
  get boxForm() {
    return this._boxForm as FormGroup;
  }
  get boxFormArray() {
    return this._boxFormArray as FormArray
  }
  get boxes(): Box[] {
    //Update boxes property
    return this._boxFormArray.controls.map((control: FormControl) => {
      return {
        name: control.get('boxType')?.value.name,
        weight: control.get('boxType')?.value.weight,
        color: control.get('boxColor')?.value.color
      }
    })
  }

}
