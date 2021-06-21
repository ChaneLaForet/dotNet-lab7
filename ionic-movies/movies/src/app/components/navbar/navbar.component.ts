import { Component, Input, ViewEncapsulation } from "@angular/core";
import { Router } from "@angular/router";

@Component({
    selector: 'app-navbar',
    templateUrl: 'navbar.component.html',
    styleUrls: ['navbar.component.scss'],
    encapsulation: ViewEncapsulation.None,
})
export class NavbarComponent {
    @Input() pageName: string;
}