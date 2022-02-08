import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from 'src/app/shared/services/admin.service';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { of } from 'rxjs';
import { Item } from 'src/app/shared/models/item';
import { DatePipe } from '@angular/common';


@Component({
  selector: 'app-orderdetails',
  templateUrl: './orderdetails.component.html',
  styleUrls: ['./orderdetails.component.css']
})
export class OrderdetailsComponent implements OnInit {

  filter:string;
  closeResult:string;
  datePipe = new DatePipe("en-UK");
  dateTime:Date;

  
  constructor(public adminService:AdminService, private router:Router,
    private modalService: NgbModal) { }

  ngOnInit(): void {
    this.adminService.getOrder();
    this.dateTime=new Date();
  }
  AdminHome(){
    this.router.navigateByUrl('admin/home')
  }

  open(content,orderId:number) {
    this.modalService.open(content,
   {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = 
         `Dismissed ${this.getDismissReason(reason)}`;
    });
    console.log(orderId);
    if(orderId!=0 || orderId!=null){
      console.log("Hi");
      this.adminService.getOrderDetailsByOrderId(orderId);
    }

  }
  
  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }

  UpdateStatus(orderId:number){
    if(orderId!=0 || orderId!=null){
      this.adminService.GetOrderById(orderId).subscribe(
        data=>{
          console.log(data.Id);
          
          this.adminService.UpdateStatus(data.Id).subscribe(
            result=>{
              console.log(result)
            }
          )
          
        }
      )
    }

  }

  //update status
  /*
  UpdateStatus(orderId:number){
    if(orderId!=0 || orderId!=null){
      this.adminService.GetOrderById(orderId).subscribe(
        data=>{
          console.log(data);
          //console.log(data.Id);
          data.forEach(item => {
            console.log(item.StatusDescriptionId);
            if(item.StatusDescriptionId == 1){
              
              console.log("Hi");
              item.StatusDescriptionId = 2;
              item.DateOfDelivery = this.dateTime;
              console.log(item);
              console.log(data);
              this.adminService.UpdateStatus(item).subscribe(
                result=>{
                  console.log(result);
                }
              );
              //console.log(item);
            }
            });
        }
      )
    }
  }
*/
}
