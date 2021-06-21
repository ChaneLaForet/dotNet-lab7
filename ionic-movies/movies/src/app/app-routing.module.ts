import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { AddMoviePage } from './pages/add-movie-page/add.movie.page';
import { LoginPage } from './pages/login/login.page';
import { MoviesPage } from './pages/movies/movies.page';

const routes: Routes = [
  {
    path: 'login',
    component: LoginPage
  },
  {
    path: 'movies',
    component: MoviesPage
  },
  {
    path: 'movies/add',
    component: AddMoviePage
  },
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
