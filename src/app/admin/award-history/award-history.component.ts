import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AwardHistory } from 'src/app/shared/models/AwardHistory';
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
  awardHistory:AwardHistory[];
  sortedAwardHistory:AwardHistory[];

  constructor(public adminService:AdminService,private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.userId=this.route.snapshot.params['UserId'];
    this.adminService.getAwardHistory(this.userId).subscribe(data=>{
      this.awardHistory = data;
      this.sortedAwardHistory=this.awardHistory.sort((a,b)=>
      new Date(b.Date).getTime()- new Date(a.Date).getTime())
    });
    this.adminService.GetEmployee(this.userId);
  }

}
