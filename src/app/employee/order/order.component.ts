import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Order } from 'src/app/shared/models/order';
import { EmployeeService } from 'src/app/shared/services/employee.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {

  filter:string;
  closeResult:string;
  UserId:number;
  orderList:Order[];

  constructor( private router:Router, private modalService: NgbModal, public employeeService:EmployeeService) { }

  ngOnInit(): void {
    this.UserId = Number(sessionStorage.getItem('userid'));
    this.employeeService.getOrderByEmployeeId(this.UserId).subscribe(
      data=>{
        console.log(data);
        this.orderList=data;
        console.log(data);
      }
    );
    
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
      this.employeeService.getOrderDetailsByOrderId(orderId);
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


}
