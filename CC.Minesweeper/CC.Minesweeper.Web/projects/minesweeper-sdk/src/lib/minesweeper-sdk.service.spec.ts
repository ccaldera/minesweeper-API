import { TestBed } from '@angular/core/testing';

import { MinesweeperSdkService } from './minesweeper-sdk.service';

describe('MinesweeperSdkService', () => {
  let service: MinesweeperSdkService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MinesweeperSdkService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
