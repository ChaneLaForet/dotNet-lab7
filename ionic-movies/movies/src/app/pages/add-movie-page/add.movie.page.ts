import { Component, ViewEncapsulation } from "@angular/core";
import { AlertController, NavController } from "@ionic/angular";
import { Movie } from "src/app/models/movie.model";
import { ApiService } from "src/app/services/api.services";
import { AuthService } from "src/app/services/auth.service";

@Component({
    selector: 'app-add-movie',
    templateUrl: 'add.movie.page.html',
    styleUrls: ['add.movie.page.scss'],
    encapsulation: ViewEncapsulation.None,
})
export class AddMoviePage {

    movie = new Movie();
    isLoggedIn: boolean;

    constructor(
        private apiSvc: ApiService,
        private navCtrl: NavController,
        private alertCtrl: AlertController,
        private authSvc: AuthService
    ) { }

    ionViewWillEnter() {
      if (this.authSvc.getToken() !== null)
        this.isLoggedIn = true;
      else this.isLoggedIn = false;
    }

    addMovie() {
      this.movie.dateAdded = new Date().toISOString();

      this.apiSvc.post('api/Movies', this.movie).subscribe(
        () => {
          this.navCtrl.pop();
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
              message: message,
              buttons: ['Ok'],
            })
            .then((al) => al.present());
        }
      );
    }

    backToMovies() {
        this.navCtrl.pop();
    }
}
