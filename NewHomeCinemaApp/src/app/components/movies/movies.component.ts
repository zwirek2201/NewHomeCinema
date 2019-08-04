import { Component, OnInit, Input } from '@angular/core';
import {MoviesService} from '../../services/movies.service';
import {Movie} from '../../models/movie';
import {Router} from '@angular/router';


@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})
export class MoviesComponent implements OnInit {

  movies:Movie[];
  movieCount:number;
  showMore:boolean = true;

  constructor(private moviesService:MoviesService, private router:Router) { }

  ngOnInit() {
    this.moviesService.GetPremieres(0,5).subscribe(m => {
    this.movies = m;
    });

    this.moviesService.GetPremieresCount().subscribe(c => {
      this.movieCount = c;
      console.log(this.movieCount);
    })
  }

  public loadMore()
  {
    this.moviesService.GetPremieres(this.movies.length,5).subscribe(m => {
      this.movies.push.apply(this.movies,m);

      if(this.movies.length == this.movieCount)
      {
        this.showMore = false;
      }
        });  
    }

    

  }
  


