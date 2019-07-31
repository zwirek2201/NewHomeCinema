import { Component, OnInit, Input } from '@angular/core';
import { MovieScreenings } from '../../models/MovieScreenings';

@Component({
  selector: 'app-screening',
  templateUrl: './screening.component.html',
  styleUrls: ['./screening.component.css']
})
export class ScreeningComponent implements OnInit {

  @Input() screening:MovieScreenings;
  
  constructor() { }

  ngOnInit() {
  }

}
