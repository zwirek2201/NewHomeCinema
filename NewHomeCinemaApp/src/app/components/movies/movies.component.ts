import { Component, OnInit, Input } from '@angular/core';
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
    this.moviesService.GetMovies(0,5).subscribe(m => {
    this.movies = m;
    });
  }

  public loadMore()
  {
    this.moviesService.GetMovies(this.movies.length,5).subscribe(m => {
      this.movies.push.apply(this.movies,m);
      });  }

}
