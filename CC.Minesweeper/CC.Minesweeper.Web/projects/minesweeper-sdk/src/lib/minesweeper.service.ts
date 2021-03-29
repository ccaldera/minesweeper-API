import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable, Inject } from "@angular/core";
import { MyServiceOptions } from "./minesweeper.config";
import { IAuthToken, IGame } from "./minesweeper.models";

@Injectable({
    providedIn: 'root'
})
export class MinesweeperService{

    private readonly apiRoot:string;

    currentToken:IAuthToken;
    tokenUrl:string;
    usersUrl:string;
    storiesUrl:string;

    constructor(@Inject('config') private config:MyServiceOptions, private http:HttpClient){
        
        this.apiRoot = this.config.serverUrl;

        this.tokenUrl = this.apiRoot + "token";
        this.usersUrl = this.apiRoot + "users/register";
        this.storiesUrl = this.apiRoot + "games";

    }

    public async getToken(username: string, password: string):Promise<IAuthToken>
    {
        var body = {
            username: username,
            password: password
        };

        var authToken = await this.http.post<IAuthToken>(this.tokenUrl, body).toPromise();

        localStorage.setItem("token", authToken.token_type + " " + authToken.access_token);

        return authToken;
    }

    public logout()
    {
        localStorage.removeItem("token");
    }

    public async register(username: string, password: string):Promise<any>
    {
        var body = {
            email: username,
            password: password
        };

        return await this.http.post(this.usersUrl, body).toPromise();
    }

    public async getGame():Promise<IGame[]>
    {
        var url = `${this.storiesUrl}/`;

        return await this.http.get<IGame[]>(url, this.getOptions()).toPromise();
    }

    public async newGame(rows:number, columns:number, mines:number):Promise<IGame>
    {
        var url = `${this.storiesUrl}/new`;

        var body = {
            rows: rows,
            columns: columns,
            mines: mines
        }; 

        return await this.http.post<IGame>(url, body, this.getOptions()).toPromise();
    }

    public revealCell(id:string, row:number, column:number){
        var url = `${this.storiesUrl}/${id}/reveal`;

        var body = {
            row: row,
            column: column
        }; 

        return this.http.patch<IGame>(url, body, this.getOptions()).toPromise();
    }

    public async flagCell(id:string, row:number, column:number){
        var url = `${this.storiesUrl}/${id}/switch-flag`;

        var body = {
            row: row,
            column: column
        }; 

        return await this.http.patch<IGame>(url, body, this.getOptions()).toPromise();
    }

    public async deleteGame(id:string):Promise<any>
    {
        var url = `${this.storiesUrl}/${id}`;

        return await this.http.delete(url, this.getOptions()).toPromise();
    }

    private getOptions(){
        var token = localStorage.getItem("token");

        let headers = new HttpHeaders()
            .set('Content-Type', 'application/json')
            .set('Authorization', token);

        return { headers: headers };
    }

}