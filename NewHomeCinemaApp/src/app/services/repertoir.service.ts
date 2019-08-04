import { Injectable } from '@angular/core';
import {MovieScreenings} from '../models/MovieScreenings';
import {Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RepertoirService {
  
  repertoirsRoute:string = environment.ApiUrl + '/repertoirs';  
  
  constructor(private http:HttpClient) { }

   public GetDayRepertoir(date:Date):Observable<MovieScreenings[]>
   {
    return this.http.get<MovieScreenings[]>(this.repertoirsRoute + '/' + date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate());
   }
}
