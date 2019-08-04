import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {MainComponent} from './components/pages/main/main.component';
import { LibraryComponent } from './components/pages/library/library.component';
import { LoginPageComponent } from './components/pages/login/login.component';

const routes: Routes = [
  {path:'home', component:MainComponent},
  {path:'login', component:LoginPageComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
