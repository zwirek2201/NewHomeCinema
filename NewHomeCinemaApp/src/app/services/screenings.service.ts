import { Injectable } from '@angular/core';
import {Screening} from '../models/Screening';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ScreeningsService {

  screeningsRoute:string = environment.ApiUrl + '/screenings';


  constructor(private http:HttpClient) { }

  public GetScreenings():Observable<Screening[]>
  {
      var url:string = this.screeningsRoute;

      return this.http.get<Screening[]>(url);
  }
}
