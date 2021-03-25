import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToasterService } from 'angular2-toaster';
import { IAuthToken } from './models/IAuthToken';
import { AuthService } from './services/auth.services';

@Component({
  selector: 'login',
  templateUrl: './login.component.html'
})
export class LoginComponent {
  title = 'Login';

  username:string;
  password: string;

  constructor(private authService:AuthService, private router: Router, private toasterService: ToasterService){}

  async login(): Promise<void>{
      
    var authToken :IAuthToken;
    
    try
    {
        authToken = <IAuthToken>await this.authService.getToken(this.username, this.password);

        localStorage.setItem("token", authToken.token_type + " " + authToken.access_token);

        this.router.navigateByUrl("/dashboard");
    }
    catch(error){
        if(error.status == 401){
          this.toasterService.pop('info', 'Invalid credentials', 'Username or password missmatch.');
            
        }
        else{
          this.toasterService.pop('error', 'Unexpected error', 'There was an error while processing your request.');
        }
    }
  }

  register(){
    this.router.navigateByUrl("/register");
  }

}
