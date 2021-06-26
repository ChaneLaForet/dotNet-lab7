import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Movie, PaginatedMovies } from '../movie.model';
import { MoviesService } from '../movies.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-list-movies',
  templateUrl: './movies-list.component.html',
  styleUrls: ['./movies-list.component.css']
})
export class MoviesListComponent {

  public movies: PaginatedMovies;
  currentPage: number;

  constructor(private moviesService: MoviesService) {

  }

  //constructor(
  //  private route: ActivatedRoute
  //) { }

  //ngOnInit() {
  //  const firstParam: string = this.route.snapshot.queryParamMap.get('page');
  //  const secondParam: string = this.route.snapshot.queryParamMap.get('perPage');
  //}

  getMovies(page: number = 1) {
    //console.log(event);
    //event.preventDefault();
    this.currentPage = page;
    this.moviesService.getMovies(page).subscribe(m => this.movies = m);
  }

  ngOnInit() {
    this.getMovies();
  }
}
