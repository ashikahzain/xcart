import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';
import { HomeComponent } from './home/home.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { UpdatecatalogueComponent } from './updatecatalogue/updatecatalogue.component';
import { HttpClientModule } from '@angular/common/http';
import { LayoutModule } from 'src/app/shared/layout/layout.module';
import { OrderdetailsComponent } from './orderdetails/orderdetails.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ItemFormComponent } from './item-form/item-form.component';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { AwardHistoryComponent } from './award-history/award-history.component';
import { AwardComponent } from './award/award.component';
import { AddAwardComponent } from './award/add-award/add-award.component';
import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
  declarations: [HomeComponent, UpdatecatalogueComponent,OrderdetailsComponent, ItemFormComponent, AwardComponent,EmployeeListComponent, AwardHistoryComponent, AddAwardComponent],



  imports: [
    CommonModule,
    AdminRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    Ng2SearchPipeModule,
    HttpClientModule,
    LayoutModule,
    NgbModule,
    NgxPaginationModule

  ]

})
export class AdminModule { }
