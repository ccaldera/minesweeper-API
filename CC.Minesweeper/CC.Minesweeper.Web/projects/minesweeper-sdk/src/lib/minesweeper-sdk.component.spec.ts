import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MinesweeperSdkComponent } from './minesweeper-sdk.component';

describe('MinesweeperSdkComponent', () => {
  let component: MinesweeperSdkComponent;
  let fixture: ComponentFixture<MinesweeperSdkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MinesweeperSdkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MinesweeperSdkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
