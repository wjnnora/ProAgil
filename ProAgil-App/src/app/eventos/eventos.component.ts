import { Evento } from './../model/evento.model';
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
  eventosFiltrados: any = [];  
  showImg = false;  
  _filtroBuscar: string = '';

  get filtroBuscar(): string{
    return this._filtroBuscar.toLocaleLowerCase();
  }
  set filtroBuscar(value: string){
    this._filtroBuscar = value;
    this.eventosFiltrados = this.filtroBuscar ? this.FiltrarEventos(this.filtroBuscar) : this.eventos;
  }

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

  FiltrarEventos(FiltrarPor: string): Evento{
    console.log("Passou por aqui.");
    return this.eventos.filter(
      (evento: Evento) => evento.tema.toLocaleLowerCase()
    );
  }
}
