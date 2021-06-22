import { Component, ViewEncapsulation } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { Movie } from 'src/app/models/movie.model';
import { ApiService } from 'src/app/services/api.services';
import { DataService } from 'src/app/services/data/data.service';

@Component({
  selector: 'app-movies',
  templateUrl: 'movies.page.html',
  styleUrls: ['movies.page.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class MoviesPage {
  //movie: Movie;
  movies: Array<Movie>; //sau movies: Movie[];

  constructor(
    private apiSvc: ApiService,
    private router: Router,
    private dataService: DataService
  ) {}

  ionViewWillEnter() {
    this.loadMovies();
  }

  goToAddMovie() {
    this.router.navigateByUrl('movies/add');
  }

  deleteMovie(movie: Movie) {
    this.apiSvc.delete(`api/Movies/${movie.id}`).subscribe(() => {
      this.loadMovies();
    });
  }

  //https://ionicacademy.com/pass-data-angular-router-ionic-4/
  //1. Using Query Params
  goToViewMovieDetails(movie: Movie) {
    let navigationExtras: NavigationExtras = {
      queryParams: {
        special: JSON.stringify(movie),
      },
    };
    this.router.navigate(['movies/view-movie/' + movie.id], navigationExtras);
  }

  /*
    //https://ionicacademy.com/pass-data-angular-router-ionic-4/
    //3. Using Extras State (new since Angular 7.2)
    goToViewMovieDetails(movie: Movie) {
      console.log("movie 1" + movie);
      let navigationExtras: NavigationExtras = {
        state: {
          movie: this.movie }};
      this.router.navigate(['movies/view-movie/' + movie.id], navigationExtras);
    }
    */

  /*

  /*
  //https://ionicacademy.com/pass-data-angular-router-ionic-4/
  //2. Service and Resolve Function
  goToViewMovieDetails(movie: Movie) {
    this.dataService.setData(movie.id, this.movie);
    this.router.navigateByUrl('movies/view-movie/' + movie.id);
  }
*/

  goToEditMovie(movie: Movie) {
    let navigationExtras: NavigationExtras = {
      queryParams: {
        special: JSON.stringify(movie),
      },
    };
    this.router.navigate(['movies/edit-movie/' + movie.id], navigationExtras);
  }

  private loadMovies() {
    this.apiSvc.get('api/Movies').subscribe((response: Array<Movie>) => {
      this.movies = response;
    });
  }
}
