import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/shared/services/admin.service';
import { Award } from 'src/app/shared/models/award';

@Component({
  selector: 'app-updateaward',
  templateUrl: './updateaward.component.html',
  styleUrls: ['./updateaward.component.css']
})
export class UpdateawardComponent implements OnInit {

  constructor(public adminService: AdminService) { }

  ngOnInit(): void {
    this.adminService.getAwards();
  }

}
