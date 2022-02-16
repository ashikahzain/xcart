import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CartComponent } from './cart/cart.component';
import { HomeComponent } from './home/home.component';
import { OrderComponent } from './order/order.component';
import { ProfileComponent } from './profile/profile.component';
import {AuthGuard} from '../shared/services/auth.guard';

const routes: Routes = [
  { path: 'employee/home', component: HomeComponent , canActivate: [AuthGuard], data: { role: 'Employee' }},
  { path: 'employee/profile', component: ProfileComponent },
  { path: 'employee/cart', component: CartComponent },
  { path: 'employee/order', component: OrderComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeRoutingModule { }
