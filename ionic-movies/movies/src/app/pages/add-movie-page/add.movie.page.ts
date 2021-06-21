import { Component, ViewEncapsulation } from "@angular/core";
import { NavController } from "@ionic/angular";
import { Movie } from "src/app/models/movie.model";
import { ApiService } from "src/app/services/api.services";

@Component({
    selector: 'app-add-movie',
    templateUrl: 'add.movie.page.html',
    styleUrls: ['add.movie.page.scss'],
    encapsulation: ViewEncapsulation.None,
})
export class AddMoviePage {

    movie = new Movie();

    constructor(
        private apiSvc: ApiService,
        private navCtrl: NavController,
        //    private alertCtrl: AlertController
    ) { }

    addMovie() {
        this.apiSvc.post('api/Movies', this.movie).subscribe(() => {
            this.navCtrl.pop();
        });
    }

    backToProducts() {
        this.navCtrl.pop();
    }
}