import { EventoService } from './../service/evento.service';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  eventos: any = [];  
  url: string = 'http://localhost:5000/Evento/';    
  showImg = false;
  filtroBuscar = '';

  constructor(private http: HttpClient, private eventoService: EventoService) { }

  ngOnInit() {
    this.getEventos();          
  }

  getEventos(){
    this.eventoService.getAllEventos().subscribe(response => {
      this.eventos = response;
    }, error => {
      console.log(error);
    });   
  }  

  getExibirImagem(){
    this.showImg = !this.showImg;
  }
}
