import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable, map, startWith } from 'rxjs';
import { Customer } from '../models/customer';
import { CustomerService } from '../customer.service';
import { slideInAnimation } from '../animation';
import { style, transition, trigger, animate } from '@angular/animations';
import { faL } from '@fortawesome/free-solid-svg-icons';
import { modalModel } from './delete-customer/modalModel';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss'],
})
export class CustomersComponent {
  loadCompelete = false;
  customers: Customer[] = [];
  public customers$ = new Observable<Customer[]>;
  public filter: FormControl = new FormControl('', { nonNullable: true });

  constructor(private customerService: CustomerService) {

    this.customers$ = customerService.getAllCustomers();
    this.customers$.subscribe(x => {
      this.customers = x;
      this.loadCompelete = true;
    });

    this.customers$ = this.filter.valueChanges.pipe(
      startWith(''),
      map(text => this.search(text))
    );

  }

  displayDeleteModal=false;
  deleteModel:Customer;
  deleteCustomer(id:string)
  {
   this.deleteModel= this.customers.find(x=>x.id==id) as Customer;
   this.displayDeleteModal=true;
  }

  showDeleteModal(hide:boolean){
    this.displayDeleteModal=hide;
  }

  search(text: string): Customer[] {
    return this.customers.filter((customer) => {
      const term = text.toLowerCase();
      return (
        customer.firstName.toLowerCase().includes(term) ||
        customer.lastName.toLowerCase().includes(term) ||
        customer.email.toLowerCase().includes(term) ||
        customer.bankAccountNumber.includes(term)
        //add filter for date!
      );
    });
  }

}

