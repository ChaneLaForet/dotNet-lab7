import { Injectable } from "@angular/core";
import { AUTH_TOKEN_LOCAL_STORAGE_KEY } from "../models/auth.model";

@Injectable()
export class AuthService {
  saveToken(token: string): void {
    localStorage.setItem(AUTH_TOKEN_LOCAL_STORAGE_KEY, token);
  }

  getToken(): string {
    return localStorage.getItem(AUTH_TOKEN_LOCAL_STORAGE_KEY);
  }

  removeToken() {
    localStorage.removeItem(AUTH_TOKEN_LOCAL_STORAGE_KEY);
  }
}
