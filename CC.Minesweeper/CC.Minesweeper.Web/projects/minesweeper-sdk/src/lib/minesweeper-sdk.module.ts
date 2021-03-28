import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { MinesweeperSdkComponent } from './minesweeper-sdk.component';



@NgModule({
  declarations: [MinesweeperSdkComponent],
  imports: [
    HttpClientModule,
    CommonModule
  ],
  exports: [MinesweeperSdkComponent]
})
export class MinesweeperSdkModule { }
