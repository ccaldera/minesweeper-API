import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard.component';
import { LoginComponent } from './login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { RegisterComponent } from './register.component';
import { CommonModule } from '@angular/common';
import { MustMatchDirective } from './must-match.validator';
import { ToasterModule } from 'angular2-toaster';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { NgxSpinnerModule } from 'ngx-spinner';
import { MinesweeperSdkModule } from 'minesweeper-sdk';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    LoginComponent,
    RegisterComponent,
    MustMatchDirective
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    BrowserAnimationsModule, 
    ToasterModule.forRoot(),
    NgxSpinnerModule,
    MinesweeperSdkModule
  ],
  providers: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
