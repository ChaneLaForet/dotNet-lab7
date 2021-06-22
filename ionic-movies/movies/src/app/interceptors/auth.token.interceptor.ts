import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AuthService } from "../services/auth.service";

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(private authSvc: AuthService) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const token = this.authSvc.getToken();
    if (token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${this.authSvc.getToken()}`,
        },
      });
    }

    return next.handle(request);
  }
}
