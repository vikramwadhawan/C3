import { Directive, forwardRef, Input } from '@angular/core';
import { AbstractControl, NG_VALIDATORS, Validator } from '@angular/forms';


@Directive({
    selector: '[appConfirmEqualValidator]',
    providers: [
        {
            provide: NG_VALIDATORS,
            useExisting: forwardRef(() => EqualValidatorDirective),
            multi: true
        }
    ]
})
export class EqualValidatorDirective implements Validator {

    @Input() appConfirmEqualValidator: string;
    validate(control: AbstractControl): { [key: string]: any } | null {
        // self value
        const controlToCompare = control.parent.get(this.appConfirmEqualValidator);
        if (controlToCompare && controlToCompare.value !== control.value) {
            console.log('no');
            return { 'notEqual': true };
        }
        return null;
    }
}