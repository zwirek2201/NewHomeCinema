import { Component, OnInit } from '@angular/core';
import {ScreeningsService} from '../../services/screenings.service';
import {Screening} from '../../models/Screening';
import {Movie} from '../../models/Movie';
import {MoviesService} from '../../services/movies.service';

@Component({
  selector: 'app-screenings',
  templateUrl: './screenings.component.html',
  styleUrls: ['./screenings.component.css']
})

export class ScreeningsComponent implements OnInit {

  screenings:Screening[];
  movies:Movie[];
  screeningOptions:ScreeningOptions[] = new ScreeningOptions[]();

  constructor(private screeningService:ScreeningsService, private moviesService:MoviesService) { }

  ngOnInit() {
    this.screeningService.GetScreenings().subscribe(s => {
      this.screenings = s;    

      this.moviesService.GetMovies().subscribe(m => {
        this.movies = m;

        for(var _i = 0; _i < this.screenings.length;_i++) 
        {
          var screening = this.screenings[_i];         
          var screeningOption = this.screeningOptions.find(s => s.Movie.id == screening.MovieId && s.AudioType == screening.AudioType && s.VideoType == screening.VideoType)
          
          if(screeningOption != null)
          {
            screeningOption.Screenings.push(screening);
          }
          else
          {
            var screeningOption = new ScreeningOptions()           
            screeningOption.Movie = this.movies.find(m => m.id == screening.MovieId)[0],
            screeningOption.AudioType = screening.AudioType,
            screeningOption.VideoType = screening.VideoType
            screeningOption.Screenings.push(screening);
          }
        }

        console.log(this.screeningOptions.length);
      });
    });


  }

}

export class ScreeningOptions {
  Movie:Movie;
  Date:Date;
  AudioType:string;
  VideoType:string;
  Screenings:Screening[];
}
