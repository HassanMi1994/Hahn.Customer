import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CustomerService } from '../../services/customer.service';
import { Customer } from '../../models/customer.model';

@Component({
  selector: 'app-customer-details',
  templateUrl: './customer-details.component.html',
  styleUrls: ['./customer-details.component.scss'],
})
export class CustomerDetailsComponent {
  isLoaded = false;
  customer: Customer | undefined;
  constructor(private customerService: CustomerService, private route: ActivatedRoute) {

    this.route.params.subscribe(params => {
      customerService.getCustomerById(params['id']).subscribe(x => {
        this.isLoaded = true;
        this.customer = x;
      });
    });
  }


}
