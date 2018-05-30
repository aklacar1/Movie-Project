import { Movie } from "./movie.type";

export class Company {
    companyId: number;
    description: string;
    image: string;
    name: string;
    movie: Movie[];
    constructor() {
    }
}