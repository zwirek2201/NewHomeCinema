import { Component, OnInit } from '@angular/core';
import {MoviesService} from '../../services/movies.service';
import {Movie} from '../../models/movie';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})
export class MoviesComponent implements OnInit {

  movies:Movie[];

  constructor(private moviesService:MoviesService) { }

  ngOnInit() {
    this.moviesService.GetMovies().subscribe(m => {
    this.movies = m;
    });
  }

}
