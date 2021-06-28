export class Film {
  id: number;
  title: string;
  description: string;
  genre: string;
  duration: string;
  yearOfRelease: number;
  director: string;
  dateAdded: string;
  rating: number;
  watched: string;
}

export class PaginatedFilms {
  firstPages: number[];
  lastPages: number[];
  previousPages: number[];
  nextPages: number[];
  totalEntities: number;
  entities: Film[];
}
