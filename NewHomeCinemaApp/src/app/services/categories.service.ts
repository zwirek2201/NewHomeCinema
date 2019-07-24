import { Injectable } from '@angular/core';
import {Category} from '../models/category';


@Injectable({
  providedIn: 'root'
})
export class CategoriesService {

  constructor() { }

  public GetCategories()
  {
      return [
        {
          id:1,
          name:'asd',
          description:'dsa'
        }
      ]
  }
}
