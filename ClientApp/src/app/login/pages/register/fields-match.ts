import { AbstractControl, FormGroup, ValidationErrors, ValidatorFn } from "@angular/forms";

/* Check if given field names have same content */
// export function FieldsMatch(controlName: string, matchingControlName: string) {
//   return (formGroup: FormGroup) => {
//     const control = formGroup.controls[controlName];
//     const matchingControl = formGroup.controls[matchingControlName];

//     // return if another validator has already found an error on the matchingControl
//     if (matchingControl.errors && !matchingControl.errors.mustMatch) return;

//     // check if values are equal and set error accordingly
//     if (control.value !== matchingControl.value) {
//         matchingControl.setErrors({ noMatch: true });
//     } else {
//         matchingControl.setErrors(null);
//     }
//   }
// }

export const passwordsMatch: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
  const password = control.get('password');
  const confirmPassword = control.get('confirmPassword');
  return password && confirmPassword && password.value === confirmPassword.value ? { passwordsMatch: true } : null;
};

// export function FieldsMatch() {
//   return (form: FormGroup) => {

//     const error =
//     form.controls["password"].value !== '' && form.controls["confirmPassword"].value
//         ? { required: true }
//         : null;
//         form.controls["confirmPassword"].setErrors(error); //<--see the setErrors
//     return error;
//   };
// }
