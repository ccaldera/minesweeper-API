import { Injectable } from '@angular/core'
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IAuthToken } from "..//models/IAuthToken";
import { IGame } from '../models/IGame';

@Injectable()
export class GamesService {

    currentToken:IAuthToken;
    storiesUrl:string = "https://localhost:5001/api/games"
        
    constructor(
        private http: HttpClient
    ){

    }

    public get():Promise<IGame[]>
    {
        var url = `${this.storiesUrl}/`;

        return this.http.get<IGame[]>(url, this.getOptions()).toPromise();
    }

    public new(rows:number, columns:number, mines:number):Promise<IGame>
    {
        var url = `${this.storiesUrl}/new`;

        var body = {
            rows: rows,
            columns: columns,
            mines: mines
        }; 

        return this.http.post<IGame>(url, body, this.getOptions()).toPromise();
    }

    public reveal(id:string, row:number, column:number){
        var url = `${this.storiesUrl}/${id}/reveal`;

        var body = {
            row: row,
            column: column
        }; 

        return this.http.patch<IGame>(url, body, this.getOptions()).toPromise();
    }

    public flag(id:string, row:number, column:number){
        var url = `${this.storiesUrl}/${id}/switch-flag`;

        var body = {
            row: row,
            column: column
        }; 

        return this.http.patch<IGame>(url, body, this.getOptions()).toPromise();
    }

    public delete(id:string):any
    {
        var url = `${this.storiesUrl}/${id}`;

        return this.http.delete(url, this.getOptions()).toPromise();
    }

    private getOptions(){
        var token = localStorage.getItem("token");

        let headers = new HttpHeaders()
            .set('Content-Type', 'application/json')
            .set('Authorization', token);

        return { headers: headers };
    }
}