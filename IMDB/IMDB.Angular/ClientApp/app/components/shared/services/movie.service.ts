import { Injectable,Inject } from '@angular/core';
import { Http, RequestOptions,Headers } from '@angular/http';
import 'rxjs/Rx';
import { Movie } from '../types/movie.type';


@Injectable()
export class MovieService {
    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {

    }
    private serviceURL :string= 'http://localhost:51346/';
    getTopRatedMovies() {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Accept': 'application/json' });
        let options = new RequestOptions({ headers: headers, withCredentials: true });
        return this.http.get('http://localhost:51346/' +'api/Movies/GetTop100RatedMovies', options)
            .map(response => response.json());

    }
    searchMovies(searchString: string) {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Accept': 'application/json' });
        let options = new RequestOptions({ headers: headers, withCredentials: true });
        console.log(this.serviceURL + 'api/Movies/SearchMovieByTitle/'+searchString);
        return this.http.get('http://localhost:51346/' +'api/Movies/SearchMovieByTitle/' + searchString, options)
            .map(response => response.json());
    }

}