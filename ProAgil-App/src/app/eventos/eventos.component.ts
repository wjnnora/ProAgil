import { EventoService } from '../_service/evento.service';
import { Component, OnInit } from '@angular/core';
import { Evento } from '../_model/evento';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
  
export class EventosComponent implements OnInit {

  eventos: Evento[];  
  eventosFiltrados: Evento[];  
  showImg = true;  
  _filtroBuscar: string;

  constructor(private eventoService: EventoService) {
    this.eventos = [];
    this.eventosFiltrados = [];
    this._filtroBuscar = '';
   }

  get filtroBuscar(): string{
    return this._filtroBuscar;
  }
  set filtroBuscar(value: string){
    this._filtroBuscar = value;
    this.eventosFiltrados = this.filtroBuscar ? this.filtrarEventos(this.filtroBuscar) : this.eventos;
  }


  ngOnInit() {
    this.getEventos();          
  }

  getEventos(){
    this.eventoService.getAllEventos().subscribe(
      (response: Evento[]) => {
      this.eventos = response;    
      this.eventosFiltrados = response;
      console.log(response);
    }, error => {
      console.log(error);
    });   
  }  

  getExibirImagem(){
    this.showImg = !this.showImg;
  }

  filtrarEventos(filtro: string): Evento[] {
    filtro = filtro.toLocaleLowerCase();    
    return this.eventos.filter(
      (evento: Evento) => evento.tema.toLocaleLowerCase().indexOf(filtro) !== -1
    );
  }
}
