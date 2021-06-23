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

  movie: Movie;
  data: any;

  constructor(private route: ActivatedRoute) {
    this.route.queryParams.subscribe((params) => {
      if (params && params.special) {
        this.movie = JSON.parse(params.special);
      }
    });
  }

}
