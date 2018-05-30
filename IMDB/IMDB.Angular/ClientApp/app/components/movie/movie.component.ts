import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MovieService } from '../shared/services/movie.service';
import { Movie } from '../shared/types/movie.type';
import { AuthService } from '../shared/services/auth.service';

@Component({
    selector: 'movie',
    templateUrl: './movie.component.html'
})
export class MovieComponent implements OnInit {
    @Input() movie: Movie;

    constructor(private movieService: MovieService, private activatedRoute: ActivatedRoute, private auth: AuthService) {
        auth.fillAuthData();
        this.movie = new Movie();
    }

    ngOnInit() {
        //this.movieService.getTopRatedMovies().then(movies => this.movies = movies);
    }
}
