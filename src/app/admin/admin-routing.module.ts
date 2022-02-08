import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from '../admin/home/home.component';
import { ItemFormComponent } from './item-form/item-form.component';
import { AwardComponent } from './award/award.component';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { AwardHistoryComponent } from './award-history/award-history.component';
import { AddPointComponent } from './add-point/add-point.component';
import { AddAwardComponent } from './award/add-award/add-award.component';
import { CatalogueComponent } from './catalogue/catalogue.component';

const routes: Routes = [
  { path: 'admin/home', component: HomeComponent },
  { path: 'admin/catalogue', component: CatalogueComponent },
  { path: 'admin/itemform', component: ItemFormComponent },
  { path: 'admin/employeeList', component: EmployeeListComponent },
  { path: 'admin/awardHistory/:UserId', component: AwardHistoryComponent },
  { path: 'admin/addPoints', component: AddPointComponent },
  { path: 'admin/award', component: AwardComponent },
  { path: 'admin/award/add', component: AddAwardComponent },
  {path: 'admin/itemform/:itemId',component:ItemFormComponent}

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
