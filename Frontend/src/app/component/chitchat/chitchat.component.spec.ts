import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChitchatComponent } from './chitchat.component';

describe('ChitchatComponent', () => {
  let component: ChitchatComponent;
  let fixture: ComponentFixture<ChitchatComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChitchatComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ChitchatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
