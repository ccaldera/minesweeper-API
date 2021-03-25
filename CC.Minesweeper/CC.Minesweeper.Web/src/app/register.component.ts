import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToasterService } from 'angular2-toaster';
import { IAuthToken } from './models/IAuthToken';
import { AuthService } from './services/auth.services';

@Component({
  selector: 'login',
  templateUrl: './register.component.html'
})
export class RegisterComponent {
  title = 'Register';

  username:string;
  password: string;
  confirmPassword: string;

  constructor(private authService:AuthService, private router: Router, private toasterService: ToasterService){}

  async register(): Promise<void>{

    if(this.password != this.confirmPassword){
        this.toasterService.pop('info', 'Invalid values', 'Password and confirmation must match.');
    }
      
    try
    {
        await this.authService.register(this.username, this.password);

        try
        {
            var authToken = <IAuthToken>await this.authService.getToken(this.username, this.password);

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
    catch(error){
        if(error.status == 409){
            this.toasterService.pop('warning', 'Duplicated user', 'User already exists.');
        }
        else if(error.status == 400){
            this.toasterService.pop('info', 'Invalid values', 'One of the input values is wrong, please validate.');
        }
        else{
            this.toasterService.pop('error', 'Unexpected error', 'There was an error while processing your request.');
        }
    }
  }

  login(){
    this.router.navigateByUrl("/login");
  }

}
