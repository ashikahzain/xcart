import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CartComponent } from './cart/cart.component';
import { HomeComponent } from './home/home.component';
import { OrderComponent } from './order/order.component';
import { ProfileComponent } from './profile/profile.component';
import {AuthGuard} from '../shared/services/auth.guard';

const routes: Routes = [
  { path: 'employee/home', component: HomeComponent , canActivate: [AuthGuard], data: { role: ['Admin','Employee'] }},
  { path: 'employee/profile', component: ProfileComponent, canActivate: [AuthGuard], data: { role: ['Admin','Employee'] } },
  { path: 'employee/cart', component: CartComponent , canActivate: [AuthGuard], data: { role: ['Admin','Employee'] }},
  { path: 'employee/order', component: OrderComponent , canActivate: [AuthGuard], data: { role: ['Admin','Employee'] }}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeRoutingModule { }
