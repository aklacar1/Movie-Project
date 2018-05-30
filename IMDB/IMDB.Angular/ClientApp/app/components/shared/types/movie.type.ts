import { Timestamp } from "rxjs";
import { Company } from "./company.type";
import { MovieStaff } from "./movie-staff.type";
import { MovieGenres } from "./movie-genres.type";
import { RatingNavigation } from "./rating-navigation.type";

export class Movie {
    movieId: number;
    companyId: number;
    duration: string;
    image: string;
    rating: number;
    releaseDate: Date;
    releaseYear: number;
    summary: string;
    title: string;
    trailerLink: string;
    company: Company;
    movieStaff: MovieStaff[];
    movieGenres: MovieGenres[];
    ratingNavigation: RatingNavigation[];
    constructor() {

    }
}