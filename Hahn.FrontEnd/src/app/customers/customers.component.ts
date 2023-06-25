import { Component, OnInit, Output, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { Customer } from '../models/customer';
import { CustomerService } from '../customer.service';
import { slideInAnimation } from '../animation';
import { style, transition, trigger, animate } from '@angular/animations';
import { modalModel } from './delete-customer/modalModel';
import { RequestModel } from '../models/RequestModel';
import { ResponseModel } from '../models/ResponseModel';
import { NgbPagination } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss'],
})
export class CustomersComponent implements OnInit {
  loadCompelete = false;
  customers: Customer[] = [];
  public customers$ = new Observable<Customer[]>;
  public reqRespones$ = new Observable<ResponseModel<Customer[]>>;
  public filter: FormControl = new FormControl('', { nonNullable: true });

  reqModel: RequestModel = new RequestModel();

  @ViewChild(NgbPagination) paginationComp!: NgbPagination;


  constructor(private customerService: CustomerService) {

  }

  public static currentPage = 1;
  public custRef=CustomersComponent;
  math = Math;
  @Output() totalRecordSize = 100;

  ngOnInit() {
    console.log(`current page is:${this.custRef.currentPage}`);
    this.loadCustomerTableData();

    //todo: cancel previous requests 
    this.filter.valueChanges.subscribe(x=>this.search(x));
  }

  pageChanged() {
   
    this.reqModel.pageNumber = CustomersComponent.currentPage;
    this.loadCustomerTableData();
  }

  private loadCustomerTableData() {
    //this.pageChanged();
    this.reqRespones$ = this.customerService.getCustomers(this.reqModel);
    this.reqRespones$.subscribe(x => {
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

  search(text: string): Customer[] {
    return this.customers.filter((customer) => {
      const term = text.toLowerCase();
      this.reqModel.search = term;
      this.loadCustomerTableData();
    });
  }
}
