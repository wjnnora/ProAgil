import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../_model/evento';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EventoService {  

  constructor(private http: HttpClient) { }

  getAllEventos(): Observable<Evento[]>{    
    return this.http.get<Evento[]>(`${environment.apiUrl}/Evento/`);
  }

  getEventoByTema(tema: string): Observable<Evento > {
    return this.http.get<Evento>(`${environment.apiUrl}/Evento/getByTema/${tema}`)
  }

  getEventoById(id: number): Observable<Evento > {
    return this.http.get<Evento>(`${environment.apiUrl}/Evento/${id}`)
  }
}
