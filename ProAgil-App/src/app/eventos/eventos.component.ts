import { EventoService } from '../_service/evento.service';
import { Component, OnInit } from '@angular/core';
import { Evento } from '../_model/evento';
import { BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, Validators } from '@angular/forms';
import { defineLocale, ptBrLocale } from 'ngx-bootstrap/chronos';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { ToastrService } from 'ngx-toastr';
defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
  
export class EventosComponent implements OnInit {
  
  titulo = 'Evento';
  eventos: Evento[] = [];
  evento: any;
  eventosFiltrados: Evento[];
  file: File;
  showImg = true;  
  _filtroBuscar: string;
  registerForm: any;  
  mensagemExcluir: string;
  currentImageName: string;

  constructor(private eventoService: EventoService, private modalService: BsModalService,
    private fb: FormBuilder, private localeService: BsLocaleService, private toastr: ToastrService) {    
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
      this.toastr.error(`Ocorreu um erro: ${error}`);      
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

  openModal(template: any, openToDelete: boolean = false) {
    if (!openToDelete) {
      this.evento = null;      
    }
    this.registerForm.reset();
    template.show(template);
  }

  editarEvento(evento: Evento, template: any) {
    this.openModal(template);    
    this.evento = Object.assign({}, evento);
    this.currentImageName = this.evento.imagePath.toString();
    this.evento.imagePath = '';
    this.registerForm.patchValue(this.evento);        
  }

  openModalExcluir(evento: Evento, template: any) {
    this.evento = evento;
    this.mensagemExcluir = `Tem certeza que deseja excluir o evento ${evento.tema.toUpperCase()}?`;
    this.openModal(template, true);
  }

  excluirEvento(eventoId: number, template: any) {
    this.eventoService.deleteEvento(eventoId).subscribe(
      () => {
        this.toastr.success("Deletado com sucesso.");
        this.getEventos();
        template.hide();
      }, error => {
        this.toastr.error(`Erro ao tentar deletar o evento: ${error}`);        
      }
    )
  }

  uploadImage() {
    const nomeArquivo = this.evento.imagePath.split("\\", 3);
    this.evento.imagePath = nomeArquivo[2];
    this.eventoService.postUpload(this.file, this.evento.imagePath).subscribe();    
  }

  salvarAlteracao(template: any) {    
    if (this.registerForm.valid) {      
      if (this.evento != null) {        
        this.evento = Object.assign({ id: this.evento.id }, this.registerForm.value);        
        this.uploadImage();
        this.eventoService.updateEvento(this.evento).subscribe(
          () => {
            this.toastr.success("Evento atualizado com sucesso.");
            template.hide();
            this.evento = null;
            this.getEventos();
          }, error => {
            this.toastr.error(`Erro ao atualizar o evento: ${error}`);            
          }
        );
      }
      else {        
        this.evento = Object.assign({}, this.registerForm.value);
        this.uploadImage();
        this.eventoService.insertEvento(this.evento).subscribe(
          () => {
            this.toastr.success("Evento inserido com sucesso.");
            template.hide();
            this.evento = null;
            this.getEventos();
          }, error => {
            this.toastr.error(`Erro ao inserir o evento: ${error}`);            
          }
        );
      }
    }
  }

  onFileChange(event: any) {
    if (event.target.files && event.target.files.length) {
      this.file = event.target.files[0];      
    }
  }
}
