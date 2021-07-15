import { EventoService } from '../_service/evento.service';
import { Component, OnInit } from '@angular/core';
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

  eventos: Evento[] = [];
  evento: Evento;
  eventosFiltrados: Evento[];  
  showImg = true;  
  _filtroBuscar: string;
  registerForm: any;
  saveMode: string;
  mensagemExcluir: string;

  constructor(private eventoService: EventoService, private modalService: BsModalService,
    private fb: FormBuilder, private localeService: BsLocaleService) {    
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

  openModal(template: any) {
    this.registerForm.reset();
    template.show(template);
  }

  editarEvento(evento: Evento, template: any) {
    this.openModal(template);
    this.evento = evento;
    this.registerForm.patchValue(evento);
    this.saveMode = 'put';
  }

  openModalExcluir(evento: Evento, template: any) {
    this.evento = evento;
    this.mensagemExcluir = `Tem certeza que deseja excluir o evento ${evento.tema.toUpperCase()}?`;
    this.openModal(template);
  }

  excluirEvento(eventoId: number, template: any) {
    this.eventoService.deleteEvento(eventoId).subscribe(
      () => {
        this.getEventos();
        template.hide();
      }, error => {
        console.log(error);
      }
    )
  }

  salvarAlteracao(template: any) {
    if (this.registerForm.valid) {      
      if (this.saveMode === 'put') {
        this.evento = Object.assign({id: this.evento.id}, this.registerForm.value);
        this.eventoService.updateEvento(this.evento).subscribe(
          response => {                    
            template.hide();
            this.getEventos();
          }, error => {
            console.log(error);
          }
        );
      }
      else {
        this.evento = Object.assign({}, this.registerForm.value);
        this.eventoService.insertEvento(this.evento).subscribe(
          response => {                    
            template.hide();
            this.getEventos();
          }, error => {
            console.log(error);
          }
        );
      }
    }
  }
}
