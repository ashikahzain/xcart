import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AwardHistoryComponent } from './award-history.component';

describe('AwardHistoryComponent', () => {
  let component: AwardHistoryComponent;
  let fixture: ComponentFixture<AwardHistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AwardHistoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AwardHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
