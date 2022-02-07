import { AbstractControl } from '@angular/forms';
import { min } from 'rxjs/operators';

export function ValidateItemName(control: AbstractControl) {
    if (!control.value) {
        return null;
    }
    const regex = new RegExp('^[A-Za-z -]+$');
    const valid = regex.test(control.value);
    return valid ? null : { invalid: true };
};

export function ValidateNumbers(control: AbstractControl) {
    if (!control.value) {
        return null;
    }
    const regex = new RegExp('^[1-9]+');
    const valid = regex.test(control.value);
    return valid ? null : { invalid: true };
};



export function ValidateImage(control: AbstractControl) {
    if (!control.value) {
        return null;
    }
    const allowedExtensions = '.jpg,.png,.jpeg';

};

