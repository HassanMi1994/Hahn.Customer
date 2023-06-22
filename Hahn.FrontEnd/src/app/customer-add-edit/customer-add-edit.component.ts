import { Component, OnInit, inject } from '@angular/core';
import { Customer } from '../models/customer';
import { ActivatedRoute } from '@angular/router';
import { CustomerService } from '../customer.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CustomerValidator } from '../models/customerValidation';
import { Location } from '@angular/common';

@Component({
  selector: 'app-customer-add-edit',
  templateUrl: './customer-add-edit.component.html',
  styleUrls: ['./customer-add-edit.component.scss']
})
export class CustomerAddEditComponent implements OnInit {
  form: FormGroup;
  customer: Customer;
  isEdit = false;
  isReady = false;
  id: number;

  constructor(private route: ActivatedRoute, private customerService: CustomerService, private fb: FormBuilder, private loacation:Location) {

  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      if (this.id != null || this.id != undefined) {
        this.isEdit = true;
        this.customerService
          .getCustomerById(this.id)
          .subscribe(cust => {
            this.customer = cust
            this.isReady = true;
            this.form.patchValue(this.customer);
          });
      }
    });

    this.form = this.fb.group({
      id: [''],
      firstName: ['',],
      lastName: [''],
      dateOfBirth: [Date,],
      email: [''],
      bankAccountNumber: ['']
    });
  }

  onSubmit() {
    var cust = new Customer(this.form.value);
    var isValidForm = this.validateForm(cust);

    if (isValidForm)
    {
      if (this.id != null || this.id !== undefined) {
        this.customerService.update(cust)
        .subscribe(response => {
          console.info(response);
        },
          error => {
            console.log(error)
          }
        );
      }
      else {
        this.customerService.create(cust)
          .subscribe(response => {
            console.info(response);
          },
            error => {
              console.log(error)
            }
          );
        }
        this.loacation.back();
        //todo: show message
    }
        
  }

  private validateForm(cust: Customer) {
    var d = new CustomerValidator();
    var errors = d.validate(cust);
    if (Object.keys(errors).length > 0) {
      console.error('error exist');
      var keyValueErrors = Object.entries(errors);
      Object.keys(errors).forEach((prop: string) => {
        var errorMessage = keyValueErrors.find(x => x[0] == prop)?.[1];
        const formControl = this.form.get(prop);
        if (formControl) {
          formControl.setErrors(
            {
              //supposed 'errors[prop]' to work, instead of error message, but did not!
              clientError: errorMessage
            });
        }
      });
    }
    return Object.keys(errors).length == 0;
  }
}
