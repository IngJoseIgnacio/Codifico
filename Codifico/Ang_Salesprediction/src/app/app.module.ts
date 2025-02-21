import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { CustomerListComponent } from './customer-list/CustomerListComponent';
import { OrdersViewComponent } from './orders-view/orders-view.component';
import { NewOrderFormComponent } from './new-order-form/new-order-form.component';
import { NewOrderComponent } from './new-order/new-order.component';

@NgModule({
  declarations: [
    AppComponent,
    CustomerListComponent,
    OrdersViewComponent,
    NewOrderFormComponent,
    NewOrderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
