export class Movie {

    id: number;
    title: string;
    description: string;
    genre: string;
    durationInMinutes: number;
    yearOfRelease: number;
    director: string;
    dateAdded?: string;
    rating: number;
    watched: boolean;

    constructor(values: Object = {}) {
      Object.assign(this, values);
    }
}
