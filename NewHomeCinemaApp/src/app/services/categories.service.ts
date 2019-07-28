import { Injectable } from '@angular/core';
import {Category} from '../models/category';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class CategoriesService {

  categoriesRoute:string = 'http://localhost:63914/api/categories';
  categories:Category[]

  constructor(private http:HttpClient) { }

  public GetCategories():Observable<Category[]>
  {
      return this.http.get<Category[]>(this.categoriesRoute);
  }

  public GetCategory(id:string):Observable<Category>
  {
      return this.http.get<Category>(this.categoriesRoute + '/' + id);
  }
}
