import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Customer } from 'src/app/models/customer.model';
import { CustomerService } from 'src/app/services/customer.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-delete-customer',
  templateUrl: './delete-customer.component.html',
  styleUrls: ['./delete-customer.component.scss']
})
export class DeleteCustomerComponent implements OnInit {
  
  constructor(private customerService:CustomerService,
    private toastr:ToastrService){

  }

  @Input() cust:Customer;
  @Output() closeTheModal=new EventEmitter<boolean>()
  display = "none";
  ngOnInit() {
  }
  openModal() {
    this.display = "block";
  }
  onCloseHandled() {
   this.closeTheModal.emit(false);
  }

  onDelete(){
    this.customerService.delete(this.cust).subscribe(response=>{
      this.toastr.warning(`${this.cust.firstName} ${this.cust.lastName} was successfuly deleted.`,'Success delete operation');
    },(error=>{
      this.toastr.warning(`${this.cust.firstName} ${this.cust.lastName} was not deleted.`,'Error in delete operation')
    }))
    this.closeTheModal.emit(false);
  }
}

