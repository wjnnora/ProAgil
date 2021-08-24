import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  titulo = 'Dashboard';

  constructor(private route: Router) { }

  ngOnInit() {
    if (localStorage.getItem('token') === null) {
      this.route.navigate(['/user/login']);
    }
  }

}
