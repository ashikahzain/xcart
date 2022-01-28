import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/shared/services/admin.service'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private orderService: AdminService) { }

  ngOnInit(): void {
    this.orderService.getOrder();
  }

}
