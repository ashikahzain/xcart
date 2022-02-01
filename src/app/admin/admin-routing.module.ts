import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from '../admin/home/home.component';
import { UpdatecatalogueComponent } from './updatecatalogue/updatecatalogue.component';

const routes: Routes = [
  { path: 'admin/home', component: HomeComponent },
  { path: 'updatecatalogue', component: UpdatecatalogueComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
