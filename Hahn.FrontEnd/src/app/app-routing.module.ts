import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomersComponent } from './customers/customers.component';
import { CustomerDetailsComponent } from './customer-details/customer-details.component';
import { TestPageComponent } from './test-page/test-page.component';
import { CustomerAddEditComponent } from './customer-add-edit/customer-add-edit.component';

const routes: Routes = [
  { component: CustomersComponent, path: 'customers', pathMatch: 'full', data: {animation: 'routeAnimations'} },
  { component: CustomerDetailsComponent, path: 'customers/details/:id', pathMatch: 'full', data: { animation: 'routeAnimations' } },
  { component: TestPageComponent, path: 'test-page', pathMatch: 'full', data: { animation: 'routeAnimations' } },
  { component: CustomerAddEditComponent, path: 'customers/new', pathMatch: 'full', data: { animation: 'routeAnimations' } },
  { component: CustomerAddEditComponent, path: 'customers/edit/:id', pathMatch: 'full', data: { animation: 'routeAnimations' } },

  { path: '**', redirectTo: '/customers', pathMatch: 'full' },
  { path: '', redirectTo: '/customers', pathMatch: 'full' }
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
