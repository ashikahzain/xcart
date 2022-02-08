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

@NgModule({
  declarations: [HomeComponent, UpdatecatalogueComponent, OrderdetailsComponent],
  imports: [
    CommonModule,
    AdminRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    Ng2SearchPipeModule,
    HttpClientModule,
    LayoutModule,
    NgbModule

  ]

})
export class AdminModule { }
