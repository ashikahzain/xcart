import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/shared/services/admin.service';
import { Award } from 'src/app/shared/models/award';
import { Router } from '@angular/router';
import { ConfirmationDialogService } from 'src/app/shared/services/confirmation-dialogue.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap'; 

@Component({
  selector: 'app-award',
  templateUrl: './award.component.html',
  styleUrls: ['./award.component.css']
})
export class AwardComponent implements OnInit {
  page: number = 1;
  confirm:boolean;
  closeResult: string;

  constructor(public adminService: AdminService, private router: Router,private confirmationDialogService: ConfirmationDialogService,private modalService: NgbModal) { }

  ngOnInit(): void {
    this.adminService.getAwards();
  }

  editAward(awardId: number) {
    console.log(awardId);
    this.router.navigate(['admin/award/form', awardId]);
  }

  deleteAward(awardId: number) {
    //if (confirm("This Item will be deleted from the awards"))
    
      // this.adminService.deleteAward(awardId).subscribe(
      //   (result) => {
      //     console.log(result);
      //     window.location.reload()
      //   }
      // );
      this.confirmationDialogService.confirm('Please Confirm', 'Do you really want to Delete the Award?','Delete','Cancel')
      .then((confirmed) => {
        if(confirmed){
          console.log('User confirmed:', confirmed);
          this.adminService.deleteAward(awardId).subscribe(
            (result) => {
              console.log(result);
              window.location.reload()
            }
          );
        }
        else{
          console.log('User confirmed:', confirmed);
        }
      })
      .catch(() => console.log('User dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));

  }
}
