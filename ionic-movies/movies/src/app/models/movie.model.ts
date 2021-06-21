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

    /*
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Genre { get; set; }
            public int DurationInMinutes { get; set; }
            public int YearOfRelease { get; set; }
            public string Director { get; set; }
            public DateTime DateAdded { get; set; }
            public float Rating { get; set; }
            public bool Watched { get; set; }
            public List<Comment> Comments { get; set; }
            public List<MovieList> MovieLists { get; set; }
    */
}