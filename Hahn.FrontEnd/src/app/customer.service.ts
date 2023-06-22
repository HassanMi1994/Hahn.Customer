import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer } from './models/customer';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
 

  private baseURL = `https://localhost:7268/api`

  constructor(private http: HttpClient) {

  }

  getAllCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(`${this.baseURL}/customers`);
  }

  getCustomerById(id:number)
  {
    return this.http.get<Customer>(`${this.baseURL}/customers/${id}`);
  }

  create(customer:Customer)
  {
    return this.http.post<Customer>(`${this.baseURL}/customers/create/`,customer);
  } 
  
  update(customer: Customer) {
    return this.http.put<Customer>(`${this.baseURL}/customers/put/`,customer)
    }
  }
