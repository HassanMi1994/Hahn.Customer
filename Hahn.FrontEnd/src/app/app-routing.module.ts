import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomersComponent } from './customers/customers.component';

const routes: Routes = [
  { component: CustomersComponent, path: 'cutomers', pathMatch: 'full' },
  { path: '**', redirectTo: '/cutomers', pathMatch: 'full' },
  { path: '', redirectTo: '/customers', pathMatch: 'full' }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
