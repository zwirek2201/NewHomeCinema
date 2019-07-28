import { Component, OnInit } from '@angular/core';
import {ScreeningsService} from '../../services/screenings.service';
import {Screening} from '../../models/Screening';

@Component({
  selector: 'app-screenings',
  templateUrl: './screenings.component.html',
  styleUrls: ['./screenings.component.css']
})

export class ScreeningsComponent implements OnInit {

  screenings:Screening[];

  constructor(private screeningService:ScreeningsService) { }

  ngOnInit() {
    this.screeningService.GetScreenings().subscribe(s => {
      this.screenings = s;
    });
  }

}
