import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmployeeRoutingModule } from './employee-routing.module';
import { HomeComponent } from './home/home.component';
import { LayoutModule } from '../shared/layout/layout.module';


@NgModule({
  declarations: [HomeComponent],
  imports: [
    CommonModule,
    EmployeeRoutingModule,
    LayoutModule
  ]
})
export class EmployeeModule { }
