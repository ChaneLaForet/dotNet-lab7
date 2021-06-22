import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { AddMoviePage } from './pages/add-movie-page/add.movie.page';
import { EditMoviePage } from './pages/edit-movie-page/edit.movie.page';
import { LoginPage } from './pages/login/login.page';
import { MoviesPage } from './pages/movies/movies.page';
import { ViewMoviePage } from './pages/view-movie-page/view.movie.page';
import { DataResolverService } from './services/resolver/data-resolver.service';

const routes: Routes = [
  {
    path: 'login',
    component: LoginPage,
  },
  {
    path: 'movies',
    component: MoviesPage,
  },
  {
    path: 'movies/add',
    component: AddMoviePage,
  },
  {
    path: 'movies/view-movie/:id',
    component: ViewMoviePage,
  },
  /* //2. Service and Resolve Function
  {
    path: 'movies/view-movie/:id',
    //component: ViewMoviePage,
    resolve: {
      special: DataResolverService,
    },
    //loadChildren: './view-movie/view-movie.module#ViewMoviePageModule',
    loadChildren:'./pages/view-movie-page/view-movie-page.module#ViewMoviePageModule',
    //loadChildren:  () => import('./pages/view-movie-page/view-movie-page.module').then(m => m.ViewMoviePageModule)
  },
  */
  {
    path: 'movies/edit-movie/:id',
    component: EditMoviePage,
  },
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules }),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
