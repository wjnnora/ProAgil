import { TabsetComponent } from 'ngx-bootstrap/tabs'; 
import { DatetimeformatpipePipe } from './../../_utils/datetimeformatpipe.pipe';
import { Component, OnInit } from '@angular/core';
import { EventoService } from '../../_service/evento.service';
import { Evento } from '../../_model/evento';
import { BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { defineLocale, ptBrLocale } from 'ngx-bootstrap/chronos';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { ToastrService } from 'ngx-toastr';
defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-evento-editar',
  templateUrl: './evento-editar.component.html',
  styleUrls: ['./evento-editar.component.css']
})
export class EventoEditarComponent implements OnInit {

  titulo = 'Editar Evento';
  registerForm: any;
  evento: any = {};  
  imagemURL = 'assets/img/upload.png';

  constructor(private eventoService: EventoService, private modalService: BsModalService,
    private fb: FormBuilder, private localeService: BsLocaleService, private toastr: ToastrService) {    
    this.localeService.use('pt-br');    
  }

  ngOnInit() {    
    this.validation();
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
      lotes: this.fb.group({
        nome: ['', [Validators.required]],        
        quantidade: ['', [Validators.required]],
        preco: ['', [Validators.required]],
        dataInicio: [''],
        dataFim: ['']
      }),
      redesSociais: this.fb.group({
        nome: ['', [Validators.required]],
        url: ['', [Validators.required]]
      })
    })
  }
  
  onFileChange(eventTarget: any) {    
    const reader = new FileReader();
    reader.onload = (event: any) => this.imagemURL = event.target.result;
    reader.readAsDataURL(eventTarget.target.files[0]);
  }

}
