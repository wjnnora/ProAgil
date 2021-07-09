import { EventoService } from '../_service/evento.service';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Evento } from '../_model/evento';
import { BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, Validators } from '@angular/forms';
import { defineLocale, ptBrLocale } from 'ngx-bootstrap/chronos';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
defineLocale('pt-br', ptBrLocale);

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
  registerForm: any;

  constructor(private eventoService: EventoService, private modalService: BsModalService,
    private fb: FormBuilder, private localeService: BsLocaleService) {
    this.eventos = [];
    this.eventosFiltrados = [];
    this._filtroBuscar = '';
    this.localeService.use('pt-br');
  }
  
  ngOnInit() {
    this.getEventos();
    this.validation();
  }
  
  get filtroBuscar(): string{
    return this._filtroBuscar;
  }
  set filtroBuscar(value: string){
    this._filtroBuscar = value;
    this.eventosFiltrados = this.filtroBuscar ? this.filtrarEventos(this.filtroBuscar) : this.eventos;
  }

  openModal(template: any) {
    template.show(template);
  }

  validation() {
    this.registerForm = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      local: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      dataEvento: ['',  [Validators.required]],
      qtdPessoas: ['', [Validators.required, Validators.min(1), Validators.max(999)]],
      imagePath: ['', [Validators.required]],
      telefone: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]]
    })
   }

  salvarAlteracao() {

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
