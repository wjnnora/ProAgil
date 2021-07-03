                                                                                          import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../model/evento.model';

@Injectable({
  providedIn: 'root'
})
export class EventoService {

  private baseUrl: string = 'http://localhost:5000';

  getAllEventos(): Observable<Evento> {
    return this.http.get<Evento>(`${this.baseUrl}/Evento/`);
  }

  constructor(private http: HttpClient) { }

}
