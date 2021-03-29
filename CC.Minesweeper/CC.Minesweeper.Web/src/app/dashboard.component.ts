import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToasterService } from 'angular2-toaster';
import { NgxSpinnerService } from 'ngx-spinner';
import * as moment from 'moment';
import { MinesweeperService } from 'projects/minesweeper-sdk/src/lib/minesweeper.service';
import { IGame } from 'projects/minesweeper-sdk/src/lib/minesweeper.models';

@Component({
  selector: 'dashboard',
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnInit{
  
  title = 'Dashboard';
  games:IGame[];
  rows: number;
  columns: number;
  mines: number;
  game:IGame;
  header:string = "Welcome!";
  interval;
  elapsedTime:string;

  constructor(private minesweeperService:MinesweeperService, private router:Router, private toasterService: ToasterService, private spinnerService: NgxSpinnerService){

  }

  ngOnInit(){
    var token = localStorage.getItem("token");

    if(!token){
        this.router.navigateByUrl("/login");
        return;
    }

    this.loadGames();
  }

  async loadGames(){

    this.spinnerService.show();

    try{
      this.games = await this.minesweeperService.getGame();
    }catch(error){
      this.toasterService.pop('error', 'Error', error.message);
    }

    this.spinnerService.hide();
  }

  async newGame(){

    this.spinnerService.show();

    try{
      this.game = await this.minesweeperService.newGame(this.rows, this.columns, this.mines);

      this.gameStatus();

      this.loadGames();
    }catch(error){
      this.toasterService.pop('error', 'Error', error.message);
    }

    this.spinnerService.hide();

  }

  changeGame(game:IGame){
    this.game = game;
    this.gameStatus();
  }

  gameStatus(){
    if(this.game.status == "InProgress"){
      this.header = "Game in progress";
    }
    if(this.game.status == "Complete"){
      this.header = "You Win!";
    }
    if(this.game.status == "Failed"){
      this.header = "You Lose!";
    }

    this.updateElapsedTime();
  }

  updateElapsedTime(){
    if(this.game != undefined){

      clearInterval(this.interval);

      if(this.game.status == "InProgress"){
        
        this.interval = setInterval(() => {
       
          var start = moment(this.game.creationDate);
          var end = moment.utc();

          var duration = moment.duration(end.diff(start));
          this.elapsedTime = moment.utc(duration.as('milliseconds')).format("HH:mm:ss");

        },1000);

      }
      else{
        
        var start = moment(this.game.creationDate);
        var end = moment(this.game.endDate);

        var duration = moment.duration(end.diff(start));
        this.elapsedTime =  moment.utc(duration.as('milliseconds')).format("HH:mm:ss");
      }

      
      
    }
    return null;
  }

  async flag(event: MouseEvent, x:number, y: number){

    event.preventDefault(); 

    if(this.game.status != "InProgress"){
      return;
    }
    
    this.spinnerService.show();

    try{

      this.game = await this.minesweeperService.flagCell(this.game.id, x, y);

    }catch(error){
      this.toasterService.pop('error', 'Error', error.message);
    }

    this.spinnerService.hide();
  }

  async reveal(x:number, y: number){

    if(this.game.status != "InProgress"){
      return;
    }

    this.spinnerService.show();

    try{
      this.game = await this.minesweeperService.revealCell(this.game.id, x, y);

      if(this.game.status == "Complete"){
        this.toasterService.pop('success', "You Win!");
      }
      else if(this.game.status == "Failed"){
        this.toasterService.pop('warning', "You Lose!");
      }

      this.gameStatus();

      var index = this.games.findIndex(x => x.id === this.game.id);
      this.games[index] = this.game;

    }catch(error){
      this.toasterService.pop('error', 'Error', error.message);
    }

    this.spinnerService.hide();
  }

  logout(){
    this.minesweeperService.logout();

    this.router.navigateByUrl("/login");
  }

}
