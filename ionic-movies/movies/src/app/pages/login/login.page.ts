import { Component, ViewEncapsulation } from "@angular/core";
import { Router } from "@angular/router";
import { Login } from "src/app/models/login.model";

@Component({
    selector: 'app-login',
    templateUrl: 'login.page.html',
    styleUrls: ['login.page.scss'],
    encapsulation: ViewEncapsulation.None,
})
export class LoginPage {

    loginData = new Login();

    constructor(private router: Router) { }

    logIn() {
        this.router.navigateByUrl('movies');
    }
}