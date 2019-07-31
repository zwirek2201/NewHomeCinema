import { Injectable } from '@angular/core';
import {MovieScreenings} from '../models/MovieScreenings';
import {Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RepertoirService {
  
  repertoirsRoute:string = 'http://localhost:5000/api/repertoirs';  
  
  constructor(private http:HttpClient) { }

   public GetDayRepertoir(date:Date):Observable<MovieScreenings[]>
   {
    return this.http.get<MovieScreenings[]>(this.repertoirsRoute + '/' + date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate());
   }
}
