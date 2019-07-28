import { Component, OnInit } from '@angular/core';
import {CategoriesService} from '../../services/categories.service';
import {Category} from '../../models/category';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {

  categories:Category[];
  private categoriesService:CategoriesService;

  constructor(private _categoriesService:CategoriesService) { 
    this.categoriesService = _categoriesService;
  }

  ngOnInit() {
    this.categoriesService.GetCategories().subscribe(cats => {
      this.categories = cats;
    });

  }

}
