import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertController, NavController } from '@ionic/angular';
import { Movie } from 'src/app/models/movie.model';
import { ApiService } from 'src/app/services/api.services';

@Component({
  selector: 'app-view-movie',
  templateUrl: 'view.movie.page.html',
  styleUrls: ['view.movie.page.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class ViewMoviePage {
  //1. Using Query Params
  movie: Movie;
  data: any;

  constructor(private route: ActivatedRoute) {
    this.route.queryParams.subscribe((params) => {
      if (params && params.special) {
        this.movie = JSON.parse(params.special);
      }
    });
  }

  /*
  //2. Service and Resolve Function
  movie: Movie;
  data: any;

  constructor(private route: ActivatedRoute, private router: Router) {}

  ngOnInit() {
    if (this.route.snapshot.data['special']) {
      this.movie = this.route.snapshot.data['special'];
    }
  }
*/

  /*
  //3. Using Extras State
  movie: Movie;
  data: any;

  constructor(private route: ActivatedRoute, private router: Router) {
    this.route.queryParams.subscribe((params) => {
      if (this.router.getCurrentNavigation().extras.state) {
        this.movie = this.router.getCurrentNavigation().extras.state.movie;
      }
    });
    console.log('Movie is' + this.movie);
  }

  ngOnInit() {}
}
*/
}
