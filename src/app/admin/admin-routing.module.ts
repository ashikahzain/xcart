import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from '../admin/home/home.component';
import { UpdatecatalogueComponent } from './updatecatalogue/updatecatalogue.component';
import {EmployeeListComponent} from './employee-list/employee-list.component';
import {AwardHistoryComponent} from './award-history/award-history.component';
import {AddPointComponent} from './add-point/add-point.component';

const routes: Routes = [
  { path: 'admin/home', component: HomeComponent },
  { path: 'updatecatalogue', component: UpdatecatalogueComponent },
  {path:'admin/employeeList',component:EmployeeListComponent},
  {path:'admin/awardHistory/:UserId',component:AwardHistoryComponent},
  {path:'admin/addPoints',component:AddPointComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
