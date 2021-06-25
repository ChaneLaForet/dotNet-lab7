export class Movie {
  id: number;
  title: string;
  description: string;
  genre: string;
  durationInMinutes: number;
  yearOfRelease: number;
  director: string;
  dateAdded: string;
  rating: number;
  watched: boolean;
}

export class PaginatedMovies {
  firstPages: number[];
  lastPages: number[];
  previousPages: number[];
  nextPages: number[];
  totalEntities: number;
  entities: Movie[];
}
