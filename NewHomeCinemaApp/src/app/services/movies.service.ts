import { Injectable } from '@angular/core';
import {Movie} from '../models/movie';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})

export class MoviesService {
  moviesRoute:string = environment.ApiUrl + '/movies';
  categoriesRoute:string = environment.ApiUrl + '/categories';  
  
  constructor(private http:HttpClient) { }

  public GetPremieres(skip:number = 0, limit:number = 0):Observable<Movie[]>
  {    
      var url:string = this.moviesRoute;

      if(skip >= 0 && limit > 0)
      {
        url += '?skip=' + skip + '&limit=' + limit;
      }

      return this.http.get<Movie[]>(url);
  }

  public GetPremieresCount():Observable<number>
  {
    return this.http.get<number>(this.moviesRoute + '/count');
  }

  public GetCategoryMovies(id:number):Observable<Movie[]>
  {
      return this.http.get<Movie[]>(this.categoriesRoute + '/' + id + '/movies');
  }

  public GetMovie(id:number):Observable<Movie>
  {
      return this.http.get<Movie>(this.moviesRoute + '/' + id);
  }
}
