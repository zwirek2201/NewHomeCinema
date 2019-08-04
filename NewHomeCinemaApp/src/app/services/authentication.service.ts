import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { UserLoginInfo } from '../models/UserLoginInfo';

@Injectable({
  providedIn: 'root'
})

export class AuthenticationService {

  public currentLoginInfo:UserLoginInfo;

  constructor(private http:HttpClient) { }

  accountsRoute:string = environment.ApiUrl + '/accounts';

  public login(email:string, passwordhash:string)
  {
    this.http.post<UserLoginInfo>(this.accountsRoute + '/login',{email, passwordhash}).subscribe(user => {
      if(user && user.token)
      {
          this.currentLoginInfo = user;
          console.log(this.currentLoginInfo);
      }
    });
  }
}
