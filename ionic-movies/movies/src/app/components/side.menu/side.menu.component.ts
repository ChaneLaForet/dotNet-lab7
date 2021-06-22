import { Component, ViewEncapsulation } from "@angular/core";
import { Router } from "@angular/router";
import { NavController } from "@ionic/angular";
import { AuthService } from "src/app/services/auth.service";

@Component({
    selector: 'app-side-menu',
    templateUrl: 'side.menu.component.html',
    styleUrls: ['side.menu.component.scss'],
    encapsulation: ViewEncapsulation.None,
})
export class SideMenuComponent {

  constructor(private authSvc: AuthService, private navCtrl: NavController) {}

  logOut() {
    this.authSvc.removeToken();
    this.navCtrl.navigateRoot('');
  }
}
