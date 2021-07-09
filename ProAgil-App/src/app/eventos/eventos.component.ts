import { EventoService } from '../_service/evento.service';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Evento } from '../_model/evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

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
  modalRef: any;

  constructor(private eventoService: EventoService, private modalService: BsModalService) {
    this.eventos = [];
    this.eventosFiltrados = [];
    this._filtroBuscar = '';    
  }
  
  ngOnInit() {
    this.getEventos();          
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  get filtroBuscar(): string{
    return this._filtroBuscar;
  }
  set filtroBuscar(value: string){
    this._filtroBuscar = value;
    this.eventosFiltrados = this.filtroBuscar ? this.filtrarEventos(this.filtroBuscar) : this.eventos;
  }

  getEventos(){
    this.eventoService.getAllEventos().subscribe(
      (response: Evento[]) => {
      this.eventos = response;    
      this.eventosFiltrados = response;      
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
