import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../model/evento.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EventoService {  

  getAllEventos(): Observable<Evento> {    
    return this.http.get<Evento>(`${environment.apiUrl}/Evento/`);
  }

  constructor(private http: HttpClient) { }

}
