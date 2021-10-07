import { Component, OnInit } from '@angular/core';
import { EventoService } from '../../_service/evento.service';
import { Evento } from '../../_model/evento';
import { FormBuilder, Validators, FormGroup, FormArray } from '@angular/forms';
import { defineLocale, ptBrLocale } from 'ngx-bootstrap/chronos';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
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

  constructor(private eventoService: EventoService, private fb: FormBuilder, private localeService: BsLocaleService, private toastr: ToastrService, private router: ActivatedRoute) {    
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
      tema: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      local: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      dataEvento: ['',  [Validators.required]],
      qtdPessoas: ['', [Validators.required, Validators.min(1), Validators.max(999)]],
      imagePath: [''],
      telefone: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      lotes: this.fb.array([this.criarLote()]),
      redesSociais: this.fb.array([this.criarRedeSocial()])
    })
  }

  carregarEvento() {
    const idEvento = this.router.snapshot.paramMap.get('id');
    if (idEvento) {
      this.eventoService.getEventoById(+idEvento)
        .subscribe(
          (evento: Evento) => {
            console.log(evento);
            this.evento = Object.assign({}, evento);
            this.currentImageName = this.evento.imagePath.toString();
            this.evento.imagePath = '';
            this.imagemURL = `http://localhost:5000/resources/images/${evento.imagePath}`;
            this.registerForm.patchValue(this.evento);
          },
          () => {
            this.toastr.error('Erro ao carregar o evento.');
          }
        )
    }    
  }

  criarLote(): FormGroup {
    return this.fb.group({
      nome: ['', [Validators.required]],
      quantidade: ['', [Validators.required]],
      preco: ['', [Validators.required]],
      dataInicio: [''],
      dataFim: ['']
    });
  }

  criarRedeSocial(): FormGroup {
    return this.fb.group({
      nome: ['', [Validators.required]],
      url: ['', [Validators.required]]
    });
  }

  adicionarLote() {
    this.lotes.push(this.criarLote());
  }

  removerLote(id: number) {
    if (this.lotes.length > 1) {
      this.lotes.removeAt(id)
    }
    else {
      this.toastr.warning('O formulário não pode ser removido.')
    }
  }

  adicionarRedeSocial() {
    this.redesSociais.push(this.criarRedeSocial());
  }

  removerRedeSocial(id: number) {
    if (this.redesSociais.length > 1) {
      this.redesSociais.removeAt(id)
    }
    else {
      this.toastr.warning('O formulário não pode ser removido.')
    }
    
  }
  
  onFileChange(eventTarget: any) {    
    const reader = new FileReader();
    reader.onload = (event: any) => this.imagemURL = event.target.result;
    reader.readAsDataURL(eventTarget.target.files[0]);
  }

}
