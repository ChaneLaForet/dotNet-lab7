import { Component, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AlertController, NavController } from '@ionic/angular';
import { Movie } from 'src/app/models/movie.model';
import { ApiService } from 'src/app/services/api.services';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-edit-movie',
  templateUrl: 'edit.movie.page.html',
  styleUrls: ['edit.movie.page.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class EditMoviePage {

  movie = new Movie();
  isLoggedIn: boolean;

  constructor(
    private apiSvc: ApiService,
    private navCtrl: NavController,
    private alertCtrl: AlertController,
    private route: ActivatedRoute,
    private authSvc: AuthService
  ) {
    this.route.queryParams.subscribe((params) => {
      if (params && params.special) {
        this.movie = JSON.parse(params.special);
      }
    });
  }

  ionViewWillEnter() {
    if (this.authSvc.getToken() !== null)
      this.isLoggedIn = true;
    else this.isLoggedIn = false;
  }

  editMovie(movie: Movie) {
    console.log(movie);
    this.apiSvc.put(`api/Movies/${this.movie.id}`, movie).subscribe(
      () => {
        this.navCtrl.back();
      },
      (err) => {
        let message = 'Validation error';
        const errorsArray = err?.error?.errors;
        if (errorsArray) {
          message = Object.values(errorsArray)[0] as string;
        }
        this.alertCtrl
          .create({
            header: 'Error',
            message,
            buttons: ['Ok'],
          })
          .then((al) => al.present());
      }
    );
  }
}
