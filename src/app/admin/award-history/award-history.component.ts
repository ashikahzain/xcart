import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AdminService } from 'src/app/shared/services/admin.service';

@Component({
  selector: 'app-award-history',
  templateUrl: './award-history.component.html',
  styleUrls: ['./award-history.component.css']
})
export class AwardHistoryComponent implements OnInit {

  filter:string;
  userId:number;
  page: number = 1;

  constructor(public adminService:AdminService,private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.userId=this.route.snapshot.params['UserId'];
    this.adminService.getAwardHistory(this.userId);
    this.adminService.GetEmployee(this.userId);
  }

}
