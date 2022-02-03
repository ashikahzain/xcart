import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LayoutRoutingModule } from './layout-routing.module';
import { NavbarComponent } from './navbar/navbar.component';
import { SidemenuComponent } from './sidemenu/sidemenu.component';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [NavbarComponent, SidemenuComponent],
  imports: [
    CommonModule,
    LayoutRoutingModule,
    Ng2SearchPipeModule,
    FormsModule
  ],
  exports: [
    NavbarComponent,
    SidemenuComponent,
  ]
})
export class LayoutModule { }
