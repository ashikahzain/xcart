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

  //Declaring Variables
  filter:string;
  userId:number;
  page: number = 1;
  awardHistory:AwardHistory[];
  sortedAwardHistory:AwardHistory[];

  constructor(public adminService:AdminService,private route:ActivatedRoute) { }

  ngOnInit(): void {
    //Getting userId from Params
    this.userId=this.route.snapshot.params['UserId'];

    //getting awardHistory of the selected employee using UserId
    this.adminService.getAwardHistory(this.userId).subscribe(data=>{
      this.awardHistory = data;

      //Sorting Awardhistory List
      this.sortedAwardHistory=this.awardHistory.sort((a,b)=>
      b.AwardId-a.AwardId)
    });

    //Getting Employee name using user Id
    this.adminService.GetEmployee(this.userId);
  }

}
