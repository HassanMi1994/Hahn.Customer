import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer } from './models/customer';
import { RequestModel } from './models/RequestModel';
import { ResponseModel } from './models/ResponseModel';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {


  private baseURL = `https://localhost:7268/api`

  constructor(private http: HttpClient) {

  }

  getCustomers(reqModel:RequestModel): Observable<ResponseModel<Customer[]>> {
    var searchPart=reqModel.search==""?"":`/search/${reqModel.search}`
    return this.http.get<ResponseModel<Customer[]>>(`${this.baseURL}/customers/page-size/${reqModel.pageSize}/page-number/${reqModel.pageNumber}${searchPart}`);
  }

  getCustomerById(id: number) {
    return this.http.get<Customer>(`${this.baseURL}/customers/${id}`);
  }

  create(customer: Customer) {
    return this.http.post<Customer>(`${this.baseURL}/customers/create/`, customer);
  }

  update(customer: Customer) {
    return this.http.put<Customer>(`${this.baseURL}/customers/put/`, customer)
  }

  delete(customer: Customer) {
    return this.http.delete<boolean>(`${this.baseURL}/customers/delete/${customer.id}`);
  }
}
