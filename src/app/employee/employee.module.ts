import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmployeeRoutingModule } from './employee-routing.module';
import { HomeComponent } from './home/home.component';
import { LayoutModule } from '../shared/layout/layout.module';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [HomeComponent],
  imports: [
    CommonModule,
    EmployeeRoutingModule,
    LayoutModule,
    Ng2SearchPipeModule,
    FormsModule
  ]
})
export class EmployeeModule { }
