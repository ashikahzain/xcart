import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LayoutRoutingModule } from './layout-routing.module';
import { NavbarComponent } from './navbar/navbar.component';
import { SidemenuComponent } from './sidemenu/sidemenu.component';


@NgModule({
  declarations: [NavbarComponent, SidemenuComponent],
  imports: [
    CommonModule,
    LayoutRoutingModule,
  ],
  exports: [
    NavbarComponent,
    SidemenuComponent
  ]
})
export class LayoutModule { }
