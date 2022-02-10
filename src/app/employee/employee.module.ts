import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmployeeRoutingModule } from './employee-routing.module';
import { HomeComponent } from './home/home.component';
import { LayoutModule } from '../shared/layout/layout.module';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProfileComponent } from './profile/profile.component';
import { CartComponent } from './cart/cart.component';
import { OrderComponent } from './order/order.component';


@NgModule({
  declarations: [HomeComponent, ProfileComponent, CartComponent, OrderComponent],
  imports: [
    CommonModule,
    EmployeeRoutingModule,
    LayoutModule,
    Ng2SearchPipeModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class EmployeeModule { }
