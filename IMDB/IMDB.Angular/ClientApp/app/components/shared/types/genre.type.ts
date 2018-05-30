import { MovieGenres } from "./movie-genres.type";

export class Genre {
    genreId: number;
    description: string;
    name: string;
    movieGenres: MovieGenres[];
    constructor() {
    }
}