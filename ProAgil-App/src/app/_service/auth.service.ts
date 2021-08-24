import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs/operators'

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  
  jwtHelper = new JwtHelperService();
  decodeToken: any;

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(`${environment.apiUrl}/user/login`, model).pipe(
      map((response: any) => {
        const user = response;        
        if (user) {
          localStorage.setItem('token', user.token);
          this.decodeToken = this.jwtHelper.decodeToken(user.token);
        }
      })
    );
  }

  register(model: any) {
    return this.http.post(`${environment.apiUrl}/user/register`, model);
  }

  loggedIn() {
    const token = localStorage.getItem('token');    
    return !this.jwtHelper.isTokenExpired(token!);
  }

}
