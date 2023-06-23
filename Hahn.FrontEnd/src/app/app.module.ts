import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FooterComponent } from './layout/footer/footer.component';
import { HeaderComponent } from './layout/header/header.component';
import { CustomersComponent } from './customers/customers.component';
import { ReactiveFormsModule, NgForm } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http'
import { SpinnerComponent } from './spinner/spinner.component';
import { LoadingInterceptor } from './interceptor/loading.interceptor';
import { BrowserAnimationsModule, provideAnimations } from '@angular/platform-browser/animations';
import { CustomerDetailsComponent } from './customer-details/customer-details.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { TestPageComponent } from './test-page/test-page.component';
import { ReadableTitleCase } from './pipes/readableTitleCase';
import { CustomerAddEditComponent } from './customer-add-edit/customer-add-edit.component';
import { ToastrModule, provideToastr } from 'ngx-toastr';
import { DeleteCustomerComponent } from './customers/delete-customer/delete-customer.component';


@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    AppComponent,
    CustomersComponent,
    SpinnerComponent,
    CustomerDetailsComponent,
    TestPageComponent,
    ReadableTitleCase,
    CustomerAddEditComponent,
    DeleteCustomerComponent,
  ],
  imports: [

    BrowserModule,
    AppRoutingModule,
    NgbModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FontAwesomeModule,
    ToastrModule.forRoot({positionClass: 'toast-bottom-left', timeOut: 5000})
  ],
exports: [
],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
  ],
    bootstrap: [AppComponent]
})
export class AppModule {


}
