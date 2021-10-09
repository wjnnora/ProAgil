import { Component, OnInit } from '@angular/core';
import { EventoService } from '../../_service/evento.service';
import { Evento } from '../../_model/evento';
import { FormBuilder, Validators, FormGroup, FormArray } from '@angular/forms';
import { defineLocale, ptBrLocale } from 'ngx-bootstrap/chronos';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-evento-editar',
  templateUrl: './evento-editar.component.html',
  styleUrls: ['./evento-editar.component.css']
})
export class EventoEditarComponent implements OnInit {

  titulo = 'Editar Evento';
  registerForm: any;
  evento: Evento = new Evento();  
  imagemURL = 'assets/img/upload.png';
  currentImageName: string;
  file: File;

  constructor(private eventoService: EventoService,
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private toastr: ToastrService,
    private activatedRouter: ActivatedRoute,
    private router: Router) {
    this.localeService.use('pt-br');    
  }

  get lotes(): FormArray {
    return <FormArray>this.registerForm.get('lotes');
  }
  
  get redesSociais(): FormArray {
    return <FormArray>this.registerForm.get('redesSociais');
  }

  ngOnInit() {    
    this.validation();
    this.carregarEvento();
  }

  validation() {
    this.registerForm = this.fb.group({
      id: [''],
      tema: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      local: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      dataEvento: ['',  [Validators.required]],
      qtdPessoas: ['', [Validators.required, Validators.min(1), Validators.max(999)]],
      imagePath: [''],
      telefone: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      lotes: this.fb.array([]),
      redesSociais: this.fb.array([])
    })
  }

  carregarEvento() {
    const idEvento = this.activatedRouter.snapshot.paramMap.get('id');
    if (idEvento) {
      this.eventoService.getEventoById(+idEvento)
        .subscribe(
          (evento: Evento) => {
            if (evento) {              
              this.evento = Object.assign({}, evento);
              this.currentImageName = this.evento.imagePath.toString();
              this.evento.imagePath = '';
              this.imagemURL = `http://localhost:5000/resources/images/${evento.imagePath}`;
              this.registerForm.patchValue(this.evento);
              this.evento.lotes.forEach(lote => {
                this.lotes.push(this.criarLote(lote));
              });
              this.evento.redesSociais.forEach(redeSocial => {
                this.redesSociais.push(this.criarRedeSocial(redeSocial));
              })
            }
            else {
              this.toastr.error('Evento não encontrado.');
              this.router.navigate(['/eventos']);
            }
          },
          () => {
            this.toastr.error('Erro ao carregar o evento.');
          }
        )
    }    
  }

  criarLote(lote?: any): FormGroup {
    return this.fb.group({
      nome: [lote ? lote.nome : '', [Validators.required]],
      quantidade: [lote ? lote.quantidade : '', [Validators.required]],
      preco: [lote ? lote.preco : '', [Validators.required]],
      dataInicio: [lote ? lote.dataInicio : ''],
      dataFim: [lote ? lote.dataFim : '']
    });
  }

  criarRedeSocial(redeSocial?: any): FormGroup {
    return this.fb.group({
      id: [redeSocial ? redeSocial.id : ''],
      nome: [redeSocial ? redeSocial.nome : '', [Validators.required]],
      url: [redeSocial ? redeSocial.url : '', [Validators.required]]
    });
  }

  adicionarLote() {
    this.lotes.push(this.criarLote());
  }

  removerLote(id: number) {    
      this.lotes.removeAt(id)
  }

  adicionarRedeSocial() {
    this.redesSociais.push(this.criarRedeSocial());
  }

  removerRedeSocial(id: number) {
    this.redesSociais.removeAt(id)
  }
  
  onFileChange(eventTarget: any) {    
    const reader = new FileReader();
    this.file = eventTarget.target.files[0];    
    reader.onload = (event: any) => this.imagemURL = event.target.result;    
    reader.readAsDataURL(this.file);
  }
  
  uploadImagem() {
    if (this.registerForm.get("imagePath").value !== '') {
      const nomeArquivo = this.evento.imagePath.split("\\", 3);
      this.evento.imagePath = nomeArquivo[2];      
      this.eventoService.postUpload(this.file, this.evento.imagePath).subscribe();
    }
  }

  salvarEvento() {
    this.evento = Object.assign({ id: this.evento.id }, this.registerForm.value);    
    this.evento.imagePath = this.registerForm.get('imagePath').value !== '' ? this.registerForm.get('imagePath').value : this.currentImageName;
    this.uploadImagem();
    this.eventoService.updateEvento(this.evento).subscribe(
      () => {
        this.toastr.success("Evento atualizado com sucesso.");        
        this.router.navigate(['/eventos']);
      }, error => {
        this.toastr.error(`Erro ao atualizar o evento: ${error}`);            
      }
    );
  }

}
