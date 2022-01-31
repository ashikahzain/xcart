import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-updatecatalogue',
  templateUrl: './updatecatalogue.component.html',
  styleUrls: ['./updatecatalogue.component.css']
})
export class UpdatecatalogueComponent implements OnInit {

  filter: string;

  constructor() { }

  ngOnInit(): void {
  }

}
