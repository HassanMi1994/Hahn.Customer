import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomersComponent } from './customers/customers.component';
import { CustomerDetailsComponent } from './customers/customer-details/customer-details.component';
import { CustomerAddEditComponent } from './customers/customer-add-edit/customer-add-edit.component';

const routes: Routes = [
  { component: CustomersComponent, path: 'customers',  },
  { component: CustomerDetailsComponent, path: 'customers/details/:id'},
  { component: CustomerAddEditComponent, path: 'customers/new'},
  { component: CustomerAddEditComponent, path: 'customers/edit/:id' },

  { path: '**', redirectTo: '/customers', pathMatch: 'full' },
  { path: '', redirectTo: '/customers', pathMatch: 'full' }
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
