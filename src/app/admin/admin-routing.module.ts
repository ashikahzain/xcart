import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from '../admin/home/home.component';
import { OrderdetailsComponent } from './orderdetails/orderdetails.component';
import { ItemFormComponent } from './item-form/item-form.component';
import { AwardComponent } from './award/award.component';
import { UpdatecatalogueComponent } from './updatecatalogue/updatecatalogue.component';
import {EmployeeListComponent} from './employee-list/employee-list.component';
import {AwardHistoryComponent} from './award-history/award-history.component';
import { AddAwardComponent } from './award/add-award/add-award.component';

const routes: Routes = [
  { path: 'admin/home', component: HomeComponent },
  { path: 'updatecatalogue', component: UpdatecatalogueComponent },
  { path: 'admin/orderdetails', component: OrderdetailsComponent },
  { path: 'admin/itemform', component: ItemFormComponent },
  {path:'admin/employeeList',component:EmployeeListComponent},
  {path:'admin/awardHistory/:UserId',component:AwardHistoryComponent},
  { path: 'admin/award', component: AwardComponent},
  { path: 'admin/award/add', component: AddAwardComponent}

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
