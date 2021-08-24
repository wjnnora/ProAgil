import { ToastrService } from 'ngx-toastr';
import { AuthService } from './../_service/auth.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(private route: Router, private toastr: ToastrService, public authService: AuthService) { }

  ngOnInit() {
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    this.toastr.success('Deslogado com sucesso.');
    this.route.navigate(['/user/login']);
  }

  enter() {
    this.route.navigate(['/user/login']);
  }

}
