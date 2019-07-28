import { Injectable } from '@angular/core';
import {Movie} from '../models/movie';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class MoviesService {

  moviesRoute:string = 'http://localhost:63914/api/movies';
  categoriesRoute:string = 'http://localhost:63914/api/categories';  
  
  constructor(private http:HttpClient) { }

  public GetMovies(skip:number = 0, limit:number = 0):Observable<Movie[]>
  {
      var url:string = this.moviesRoute;

      if(skip >= 0 && limit > 0)
      {
        url += '?skip=' + skip + '&limit=' + limit;
      }

      return this.http.get<Movie[]>(url);
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
