import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { Film, PaginatedFilms } from './films.model';


@Injectable({
  providedIn: 'root'
})
export class FilmsService {

  private apiUrl: string;

  constructor(private httpClient: HttpClient, @Inject('API_URL') apiUrl: string) {
    this.apiUrl = apiUrl;
  }

  getFilms(): Observable<Film[]> {
    return this.httpClient.get<Film[]>(this.apiUrl + 'film');
  }
/*  getFilms(page: number): Observable<PaginatedFilms> {
    return this.httpClient.get<PaginatedFilms>(this.apiUrl + 'film', { params: { 'page': page } });
  }*/
}
