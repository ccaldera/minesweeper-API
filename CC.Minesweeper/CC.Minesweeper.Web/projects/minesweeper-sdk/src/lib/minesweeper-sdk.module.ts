import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ModuleWithProviders, NgModule } from '@angular/core';
import { MyServiceOptions } from './minesweeper.config';
import { MinesweeperService } from './minesweeper.service';

@NgModule({
  declarations: [],
  imports: [
    HttpClientModule,
    CommonModule
  ],
  exports: [],
  providers: [MinesweeperService],
})
export class MinesweeperSdkModule {
  static forRoot(config: MyServiceOptions): ModuleWithProviders<MinesweeperSdkModule> {
    // User config get logged here
    console.log(config);
    return {
      ngModule: MinesweeperSdkModule,
      providers: [MinesweeperService, {provide: 'config', useValue: config}]
    };
  }
}