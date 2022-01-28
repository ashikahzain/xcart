import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdatecatalogueComponent } from './updatecatalogue.component';

describe('UpdatecatalogueComponent', () => {
  let component: UpdatecatalogueComponent;
  let fixture: ComponentFixture<UpdatecatalogueComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UpdatecatalogueComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdatecatalogueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
