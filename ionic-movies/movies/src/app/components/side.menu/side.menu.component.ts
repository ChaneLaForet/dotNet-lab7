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
export class SideMenuComponent  {

  isLoggedIn: boolean;

  constructor(private authSvc: AuthService, private navCtrl: NavController, private router: Router) {}

  ionViewWillEnter() {
    if (this.authSvc.getToken() !== null)
      this.isLoggedIn = true;
    else this.isLoggedIn = false;
  }

  logIn() {
    this.router.navigateByUrl('/login');
  }

  logOut() {
    this.authSvc.removeToken();
    this.navCtrl.navigateRoot('');
  }

  private checkAuthorization() {
    if (this.authSvc.getToken() !== null)
      this.isLoggedIn = true;
    else this.isLoggedIn = false;
  }
}
