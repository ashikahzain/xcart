import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/shared/services/admin.service';
import { Award } from 'src/app/shared/models/award';
import { Router } from '@angular/router';

@Component({
  selector: 'app-award',
  templateUrl: './award.component.html',
  styleUrls: ['./award.component.css']
})
export class AwardComponent implements OnInit {

  constructor(public adminService: AdminService, private router: Router) { }

  ngOnInit(): void {
    this.adminService.getAwards();
  }

  editAward(awardId: number) {
    console.log(awardId);
    this.router.navigate(['admin/award/form', awardId]);
  }

  deleteAward(awardId: number) {
    if (confirm("This Item will be deleted from the catalogue"))
    this.adminService.deleteAward(awardId).subscribe(
      (result) => {
        window.location.reload()
      }
    );
  }

}
