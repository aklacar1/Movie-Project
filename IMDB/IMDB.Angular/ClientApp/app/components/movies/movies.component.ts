import { Component, Input, OnInit,Inject } from '@angular/core';
import { ActivatedRoute,Router } from '@angular/router';
import { MovieService } from '../shared/services/movie.service';
import { Movie } from '../shared/types/movie.type';
import { AuthService } from '../shared/services/auth.service';


@Component({
    selector: 'movies',
    templateUrl: './movies.component.html'
})
export class MoviesComponent implements OnInit {
    @Input() movies: Movie[] = new Array<Movie>();
    searchText: string = '';
    constructor(private movieService: MovieService, private auth: AuthService) {
        auth.fillAuthData();
    }

    ngOnInit() {
        this.getTopRatedMovies();
    }

    searchMovies() {
        this.movieService.searchMovies(this.searchText).map(res => res as Movie[]).toPromise().then(movies => this.movies = movies);
    }

    getTopRatedMovies() {
        console.log("getTopRated");
        this.movieService.getTopRatedMovies().map(res => res as Movie[]).toPromise().then(movies => this.movies = movies);
    }
}
