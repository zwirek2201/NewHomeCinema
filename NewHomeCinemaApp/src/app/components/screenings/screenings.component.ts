import { Component, OnInit } from '@angular/core';
import {Screening} from '../../models/Screening';
import {Movie} from '../../models/Movie';
import {MovieScreenings} from '../../models/MovieScreenings';
import {RepertoirService} from '../../services/repertoir.service';

@Component({
  selector: 'app-screenings',
  templateUrl: './screenings.component.html',
  styleUrls: ['./screenings.component.css']
})

export class ScreeningsComponent implements OnInit {

  movieScreenings:MovieScreenings[];

  constructor(private repertoirService:RepertoirService) { }

  ngOnInit() {
    var d = new Date();
    
    this.repertoirService.GetDayRepertoir(d).subscribe(r => {
      this.movieScreenings = r;
      console.log(this.movieScreenings);
    });
  }

}


