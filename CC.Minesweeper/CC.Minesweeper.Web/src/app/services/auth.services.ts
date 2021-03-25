import { Injectable } from '@angular/core'
import { HttpClient } from '@angular/common/http';
import { IAuthToken } from "..//models/IAuthToken";

@Injectable()
export class AuthService {

    currentToken:IAuthToken;
    tokenUrl:string = "https://localhost:5001/api/token"
    usersUrl:string = "https://localhost:5001/api/users/register"
        
    constructor(
        private http: HttpClient
    ){

    }

    public getToken(username: string, password: string):Promise<IAuthToken>
    {
        var body = {
            username: username,
            password: password
        };

        return this.http.post<IAuthToken>(this.tokenUrl, body).toPromise();
    }

    public register(username: string, password: string):Promise<any>
    {
        var body = {
            email: username,
            password: password
        };

        return this.http.post(this.usersUrl, body).toPromise();
    }
}