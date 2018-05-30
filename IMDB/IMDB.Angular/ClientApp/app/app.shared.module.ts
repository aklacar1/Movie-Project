import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { TableModule } from 'primeng/table';

import { MovieService } from './components/shared/services/movie.service';
import { AuthService } from './components/shared/services/auth.service';

import { AppComponent } from './components/app/app.component';
import { MoviesComponent } from './components/movies/movies.component';
import { HomeComponent } from './components/home/home.component';
import { NavigationComponent } from './components/shared/navbar/navigation.component';
import { MovieComponent } from './components/movie/movie.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { FaqComponent } from './components/FAQ/faq.component';




@NgModule({
    bootstrap: [AppComponent],
    declarations: [
        AppComponent,
        MoviesComponent,
        HomeComponent,
        NavigationComponent,
        MovieComponent,
        LoginComponent,
        RegisterComponent,
        FaqComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'login', component: LoginComponent },
            { path: 'signup', component: RegisterComponent },
            { path: 'register', component: RegisterComponent },
            { path: 'faq', component: FaqComponent },
            { path: 'movies', component: MoviesComponent },
            { path: 'movie:id', component: MovieComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        MovieService,
        AuthService
    ]
})
export class AppModuleShared {
}
