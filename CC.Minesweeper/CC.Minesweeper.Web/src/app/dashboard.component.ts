import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToasterService } from 'angular2-toaster';
import { NgxSpinnerService } from 'ngx-spinner';
import { IGame } from './models/IGame';
import { GamesService } from './services/games.services';

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

  constructor(private gamesService:GamesService, private router:Router, private toasterService: ToasterService, private spinnerService: NgxSpinnerService){

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
    try{
      this.games = await this.gamesService.get();
    }catch(error){
      this.toasterService.pop('error', 'Error', error.message);
    }
  }

  async newGame(){
    try{
      this.game = await this.gamesService.new(this.rows, this.columns, this.mines);

      this.gameStatus();

      this.loadGames();
    }catch(error){
      this.toasterService.pop('error', 'Error', error.message);
    }
  }

  changeGame(game:IGame){
    this.game = game;
    this.gameStatus();
  }

  gameStatus(){
    if(this.game.status == "InProgress")
      this.header = "Game in progress";
    if(this.game.status == "Complete")
      this.header = "You Win!";
    if(this.game.status == "Failed")
      this.header = "You Lose!";
  }

  async flag(event: MouseEvent, x:number, y: number){

    event.preventDefault(); 

    if(this.game.status != "InProgress"){
      return;
    }
    
    try{
      this.game = await this.gamesService.flag(this.game.id, x, y);

    }catch(error){
      this.toasterService.pop('error', 'Error', error.message);
    }
  }

  async reveal(x:number, y: number){

    if(this.game.status != "InProgress"){
      return;
    }

    try{
      this.game = await this.gamesService.reveal(this.game.id, x, y);

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
  }

  logout(){
    localStorage.removeItem("token");

    this.router.navigateByUrl("/login");
  }

}