import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateawardComponent } from './updateaward.component';

describe('UpdateawardComponent', () => {
  let component: UpdateawardComponent;
  let fixture: ComponentFixture<UpdateawardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UpdateawardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateawardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
