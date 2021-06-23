import { Component, ViewEncapsulation } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { BooleanValueAccessor } from '@ionic/angular';
import { Movie } from 'src/app/models/movie.model';
import { ApiService } from 'src/app/services/api.services';
import { AuthService } from 'src/app/services/auth.service';
import { DataService } from 'src/app/services/data/data.service';

@Component({
  selector: 'app-movies',
  templateUrl: 'movies.page.html',
  styleUrls: ['movies.page.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class MoviesPage {

  movies: Array<Movie>;
  isLoggedIn: boolean;

  constructor(
    private apiSvc: ApiService,
    private router: Router,
    private dataService: DataService,
    private authSvc: AuthService
  ) {}


  ionViewWillEnter() {
    this.loadMovies();
    if (this.authSvc.getToken() !== null)
      this.isLoggedIn = true;
    else this.isLoggedIn = false;
  }


/*
  ionViewWillEnter() {
    this.movies = [
        {
            id: 1,
            title: 'title',
            description: 'description',
            genre: 'genre',
            durationInMinutes: 10,
            yearOfRelease: 1999,
            director: 'director',
            dateAdded: '2015-03-15T18:50:00',
            rating: 10,
            watched: true
        },
        {
            id: 2,
            title: 'title2',
            description: 'description2',
            genre: 'genre2',
            durationInMinutes: 9,
            yearOfRelease: 2000,
            director: 'director2',
            dateAdded: '2013-03-15T18:50:00',
            rating: 9,
            watched: true
        }
    ];
}
*/

  goToAddMovie() {
    this.router.navigateByUrl('movies/add');
  }

  deleteMovie(movie: Movie) {
    this.apiSvc.delete(`api/Movies/${movie.id}`).subscribe(() => {
      this.loadMovies();
    });
  }

  goToViewMovieDetails(movie: Movie) {
    let navigationExtras: NavigationExtras = {
      queryParams: {
        special: JSON.stringify(movie),
      },
    };
    this.router.navigate(['movies/view-movie/' + movie.id], navigationExtras);
  }

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
