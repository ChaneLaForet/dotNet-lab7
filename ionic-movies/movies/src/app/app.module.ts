import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';

import { IonicModule, IonicRouteStrategy } from '@ionic/angular';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { LoginPage } from './pages/login/login.page';
import { MoviesPage } from './pages/movies/movies.page';
import { NavbarComponent } from './components/navbar/navbar.component';
import { SideMenuComponent } from './components/side.menu/side.menu.component';
import { ApiService } from './services/api.services';
import { HttpClientModule } from '@angular/common/http';
import { AddMoviePage } from './pages/add-movie-page/add.movie.page';
import { FormsModule } from '@angular/forms';
import { ViewMoviePage } from './pages/view-movie-page/view.movie.page';
import { EditMoviePage } from './pages/edit-movie-page/edit.movie.page';
import { AuthService } from './services/auth.service';

@NgModule({
  declarations: [
    //components
    AppComponent,
    NavbarComponent,
    SideMenuComponent,
    //pages
    AddMoviePage,
    LoginPage,
    MoviesPage,
    ViewMoviePage,
    EditMoviePage,
  ],
  entryComponents: [],
  imports: [
    BrowserModule,
    IonicModule.forRoot(),
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
  ],
  providers: [
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
    ApiService,
    AuthService
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
