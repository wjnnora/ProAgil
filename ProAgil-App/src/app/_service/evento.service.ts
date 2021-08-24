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
    return this.http.get<Evento[]>(`${environment.apiUrl}/evento/`);
  }

  getEventoByTema(tema: string): Observable<Evento> {
    return this.http.get<Evento>(`${environment.apiUrl}/evento/getbytema/${tema}`);
  }

  getEventoById(id: number): Observable<Evento> {
    return this.http.get<Evento>(`${environment.apiUrl}/evento/${id}`);
  }

  insertEvento(evento: Evento): Observable<Evento> {    
    return this.http.post<Evento>(`${environment.apiUrl}/evento/`, evento);
  }

  updateEvento(evento: Evento): Observable<Evento> {
    return this.http.put<Evento>(`${environment.apiUrl}/evento/${evento.id}`, evento);
  }
  
  deleteEvento(eventoId: number): Observable<Evento> {
    return this.http.delete<Evento>(`${environment.apiUrl}/evento/${eventoId}`);
  }

  postUpload(file: File, fileName: string) {
    const fileToUpload = <File>file;
    const formData = new FormData();    
    formData.append('file', fileToUpload, fileName);    
    return this.http.post(`${environment.apiUrl}/evento/upload/`, formData);
  }
}
