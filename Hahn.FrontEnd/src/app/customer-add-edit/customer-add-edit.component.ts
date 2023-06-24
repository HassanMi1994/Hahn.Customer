import { Component, OnInit, inject } from '@angular/core';
import { Customer } from '../models/customer';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { CustomerService } from '../customer.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CustomerValidator } from '../models/customerValidation';
import { Location } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { ValidationErrors } from 'fluentvalidation-ts';

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

  constructor(
    private customerService: CustomerService,
    private fb: FormBuilder,
    private loacation: Location,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService) {
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
      dateOfBirth: ['',],
      email: [''],
      bankAccountNumber: ['']
    });
  }

  onSubmit() {
    var cust = new Customer(this.form.value);
    var isValidForm = this.validateForm(cust);

    if (isValidForm) {
      if (this.id != null || this.id !== undefined) {
        this.customerService.update(cust)
          .subscribe(response => {
            console.log('successfuly updated');
            this.toastr.success(`customer '${this.customer.firstName} ${this.customer.lastName}' successfuly updated!`, 'Success operation!');
            this.router.navigateByUrl('/customers');
          },
            error => {
              console.log(error);
              this.checkErrors(error.error);
            }
          );
      }
      else {
        this.customerService.create(cust)
          .subscribe(response => {
            console.log('successfuly inserted');
            this.toastr.success(`New customer '${this.customer.firstName} ${this.customer.lastName}' successfuly added!`, 'Success operation!');
            this.router.navigateByUrl('/customers');
          },
            error => {
              console.log(error)
              this.checkErrors(error.error);
            }
          );
      }
    }

  }

  private validateForm(cust: Customer) {
    var d = new CustomerValidator();
    var errors = d.validate(cust);
    this.checkErrors(errors);
    return Object.keys(errors).length == 0;
  }

  private checkErrors(errors:ValidationErrors<Customer>) {
    if (Object.keys( errors).length > 0) {
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
  }
}
