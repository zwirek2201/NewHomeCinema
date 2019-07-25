import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CategoriesComponent } from './components/categories/categories.component';

import {CategoriesService} from './services/categories.service';
import {MoviesService} from './services/movies.service';

import {HttpClientModule} from '@angular/common/http';
import { MainComponent } from './components/pages/main/main.component';
import { NavbarComponent } from './components/layout/navbar/navbar.component';
import { MoviesComponent } from './components/movies/movies.component';
import { MovieComponent } from './components/movie/movie.component';

@NgModule({
  declarations: [
    AppComponent,
    CategoriesComponent,
    MainComponent,
    NavbarComponent,
    MoviesComponent,
    MovieComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [CategoriesService, MoviesService],
  bootstrap: [AppComponent]
})
export class AppModule { }
