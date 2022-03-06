import { Component, Inject, Input, OnInit } from '@angular/core';
import { Film, Comment, PaginatedFilms } from '../films.model';
import { FilmsService } from '../films.service';


@Component({
  selector: 'app-list-films',
  templateUrl: './films-list.component.html',
  styleUrls: ['./films-list.component.css']
})
export class FilmsListComponent {

  public films: Film[]; public comments: Comment[];
  //public films: PaginatedFilms;
  //currentPage: number;

  constructor(private filmsService: FilmsService) {
    
  }

  getFilms() {
    this.filmsService.getFilms().subscribe(p => this.films = p);
  }

/*  getFilms(page: number = 1) {
    this.currentPage = page;
    this.filmsService.getFilms(page).subscribe(f => this.films = f);
  }*/

  ngOnInit() {
    this.getFilms();
  }

}
