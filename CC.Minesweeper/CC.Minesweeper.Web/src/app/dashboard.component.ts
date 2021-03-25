import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToasterService } from 'angular2-toaster';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'dashboard',
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent{
  
  title = 'Dashboard';

  constructor(private router:Router, private toasterService: ToasterService, private spinnerService: NgxSpinnerService){

  }
}
