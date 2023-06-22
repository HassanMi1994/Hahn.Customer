import { Validator } from 'fluentvalidation-ts';
import {Customer} from './customer'
export class CustomerValidator extends Validator<Customer>
{
    constructor(){
        super();

        this.ruleFor('firstName')
             .notEmpty()
             .withMessage("First name is a required field.");

        this.ruleFor('firstName')
            .minLength(5)
            .withMessage('name has to be at least 5 character')

        this.ruleFor('lastName')
            .minLength(5)
            .maxLength(30)
            .withMessage('First name should be at least 5 characters and maximum should be 30');

        this.ruleFor('lastName')
            .notEmpty()
            .withMessage('Last Name is required');
        this.ruleFor('lastName')
            .minLength(5)
            .maxLength(30)
            .withMessage("Last name should be at least 5 characters and maximum should be 30");

        this.ruleFor('email')
            .notEmpty()
            .withMessage('Email is required field');

        this.ruleFor('email')
            .emailAddress()
            .withMessage('Please enter a valid email');

      // this.ruleFor('dateOfBirth').setValidator())

       this.ruleFor('bankAccountNumber').notEmpty().withMessage("Bank account number is required");
       this.ruleFor('bankAccountNumber').minLength(12).withMessage("Bank account number should be at least 12 characters");
    }
}

class DateValidator extends Validator<Date>
{
    constructor(){
        super();

    }
}