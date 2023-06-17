import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss']
})
export class CustomersComponent {

  customers = [
    {
      firstName:'Hassan',
      lastName: 'Monjezi',
      dateOfBirth: new Date(1994,9,14),
      email: 'hassan.mi1994@gmail.com',
      bankAccountNumber: '6037997487048924'
    },
    {
      firstName:'Alex',
      lastName: 'Javadi',
      dateOfBirth: new Date(2002,9,14),
      email: 'amirAli@gmail.com',
      bankAccountNumber: '6037997487048924'
    },
    {
      firstName:'James',
      lastName: 'Sharp',
      dateOfBirth: new Date(1895,9,14),
      email: 'James.Sharp@gmail.com',
      bankAccountNumber: '23498325925928448'
    }
  ]

 public filter:FormControl = new FormControl('', { nonNullable: true });
}
