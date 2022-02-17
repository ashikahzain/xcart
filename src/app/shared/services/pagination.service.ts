import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PaginationService {
  temppage: number = 0;
  pageField = [];
  exactPageList: any;
  constructor() { }

  // On page load 

  pageOnLoad() { 
    if (this.temppage == 0) {
      this.pageField = [];
      for (var a = 0; a < this.exactPageList; a++) {
        this.pageField[a] = this.temppage + 1;
        this.temppage = this.temppage + 1;
      }
    }
  }
 
}
