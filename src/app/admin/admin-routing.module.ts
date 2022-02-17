import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from '../admin/home/home.component';
import { OrderdetailsComponent } from './orderdetails/orderdetails.component';
import { ItemFormComponent } from './item-form/item-form.component';
import { AwardComponent } from './award/award.component';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { AwardHistoryComponent } from './award-history/award-history.component';
import { AddAwardComponent } from './award/add-award/add-award.component';
import { CatalogueComponent } from './catalogue/catalogue.component';
import { AuthGuard } from '../shared/services/auth.guard';

const routes: Routes = [
  { path: 'admin/home', component: HomeComponent , canActivate: [AuthGuard], data: { role: ['Admin'] }},
  { path: 'admin/catalogue', component: CatalogueComponent, canActivate: [AuthGuard], data: { role: ['Admin'] } },
  { path: 'admin/orderdetails', component: OrderdetailsComponent, canActivate: [AuthGuard], data: { role: ['Admin'] } },
  { path: 'admin/itemform', component: ItemFormComponent , canActivate: [AuthGuard], data: { role: ['Admin'] }},
  { path: 'admin/employeeList', component: EmployeeListComponent, canActivate: [AuthGuard], data: { role: ['Admin'] } },
  { path: 'admin/awardHistory/:UserId', component: AwardHistoryComponent, canActivate: [AuthGuard], data: { role: ['Admin'] } },
  { path: 'admin/award', component: AwardComponent , canActivate: [AuthGuard], data: { role: ['Admin'] }},
  { path: 'admin/award/form', component: AddAwardComponent , canActivate: [AuthGuard], data: { role: ['Admin'] }},
  { path: 'admin/award/form/:awardId', component: AddAwardComponent , canActivate: [AuthGuard], data: { role: ['Admin'] }},
  {path: 'admin/itemform/:itemId',component:ItemFormComponent, canActivate: [AuthGuard], data: { role: ['Admin'] }},


];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
