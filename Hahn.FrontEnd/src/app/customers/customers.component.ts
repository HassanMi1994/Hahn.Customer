import { Component, OnInit, Output, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable, Subscription, of } from 'rxjs';
import { Customer } from '../models/customer.model';
import { CustomerService } from '../services/customer.service';
import { ResponseModel } from '../models/response-model';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss'],
})
export class CustomersComponent implements OnInit {

  //#region variables
  loadCompelete = false;
  customers: Customer[] = [];
  public customers$ = new Observable<Customer[]>;
  public reqRespones$ = new Observable<ResponseModel<Customer[]>>;
  public filter: FormControl = new FormControl('', { nonNullable: true });
  public currentPage = 1;
  math = Math;
  private lastSearchRequest: Subscription;
  @Output() totalRecordSize = 100;
  //#endregion

  constructor(public customerService: CustomerService) {
    this.currentPage = this.customerService.paginationStore.pageNumber;

  }

  ngOnInit() { 
    this.loadCustomerTableData();
    this.filter.setValue(this.customerService.paginationStore.search)
    this.filter.valueChanges.subscribe(x => this.search(x));
  }

  pageChanged() {
    this.customerService.paginationStore.pageNumber = this.currentPage;
    this.loadCustomerTableData();
  }

  private loadCustomerTableData() {
    if (this.lastSearchRequest && !this.lastSearchRequest.closed) {
      this.lastSearchRequest.unsubscribe();
    }
    this.reqRespones$ = this.customerService.getCustomers();

    this.lastSearchRequest = this.reqRespones$.subscribe(x => {
      this.customers = x.data;
      this.customers$ = of(x.data);
      this.totalRecordSize = x.totalSize;
      this.loadCompelete = true;
    });
  }

  displayDeleteModal = false;
  deleteModel: Customer;
  deleteCustomer(id: string) {
    this.deleteModel = this.customers.find(x => x.id == id) as Customer;
    this.displayDeleteModal = true;
  }

  showDeleteModal(hide: boolean) {
    this.displayDeleteModal = hide;
    this.loadCustomerTableData();
  }

  search(text: string) {
    const term = text.toLowerCase();
    this.customerService.paginationStore.search = term;
    this.loadCustomerTableData();
  };
}

