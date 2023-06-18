import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable, map, startWith } from 'rxjs';


interface Customer {
  firstName: string,
  lastName: string,
  dateOfBirth: Date,
  email: string,
  bankAccountNumber: string
}

const customers: Customer[] = [

  {
    firstName: 'Hassan',
    lastName: 'Monjezi',
    dateOfBirth: new Date(1994, 9, 14),
    email: 'hassan.mi1994@gmail.com',
    bankAccountNumber: '6037997487048924'
  },
  {
    firstName: 'Alex',
    lastName: 'Javadi',
    dateOfBirth: new Date(2002, 9, 14),
    email: 'amirAli@gmail.com',
    bankAccountNumber: '6037997487048924'
  },
  {
    firstName: 'James',
    lastName: 'Sharp',
    dateOfBirth: new Date(1895, 9, 14),
    email: 'James.Sharp@gmail.com',
    bankAccountNumber: '23498325925928448'
  }

]


@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss']
})
export class CustomersComponent {
  public customers$ = new Observable<Customer[]>;
  public filter: FormControl = new FormControl('', { nonNullable: true });

  constructor() {
    this.customers$= this.filter.valueChanges.pipe(
     startWith(''),
      map(text => search(text))
    );

  }
}

function search(text: string): Customer[] {
  return customers.filter((customer) => {
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
