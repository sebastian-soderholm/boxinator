import { AbstractControl, FormGroup, ValidationErrors, ValidatorFn } from "@angular/forms";

/* Check if given field names have same content */
export function FieldsMatch(controlName: string, matchingControlName: string) {
  return (formGroup: FormGroup) => {
    const control = formGroup.controls[controlName];
    const matchingControl = formGroup.controls[matchingControlName];

    // return if another validator has already found an error on the matchingControl
    if (matchingControl.errors && !matchingControl.errors.mustMatch) return;

    // check if values are equal and set error accordingly
    if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ mustMatch: true });
    } else {
        matchingControl.setErrors(null);
    }
}
}
