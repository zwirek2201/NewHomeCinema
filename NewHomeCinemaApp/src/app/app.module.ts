import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CategoriesComponent } from './components/categories/categories.component';

import {CategoriesService} from './services/categories.service';
import {MoviesService} from './services/movies.service';
import {AuthenticationService} from './services/authentication.service';

import {HttpClientModule} from '@angular/common/http';
import { MainComponent } from './components/pages/main/main.component';
import { NavbarComponent } from './components/layout/navbar/navbar.component';
import { MoviesComponent } from './components/movies/movies.component';
import { MovieComponent } from './components/movie/movie.component';
import { LibraryComponent } from './components/pages/library/library.component';
import { ScreeningsComponent } from './components/screenings/screenings.component';
import { ScreeningComponent } from './components/screening/screening.component';
import { LoginInfoComponent } from './components/layout/login-info/login-info.component';
import { LoginPageComponent } from './components/pages/login/login.component';
import { LoginComponent } from './components/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    AppComponent,
    CategoriesComponent,
    MainComponent,
    NavbarComponent,
    MoviesComponent,
    MovieComponent,
    LibraryComponent,
    ScreeningsComponent,
    ScreeningComponent,
    LoginInfoComponent,
    LoginPageComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [CategoriesService, MoviesService, AuthenticationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
