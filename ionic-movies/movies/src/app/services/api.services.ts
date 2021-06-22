import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Movie } from '../models/movie.model';
//import { map, filter, switchMap } from 'rxjs/operators';
//import 'rxjs/add/observable/of';

@Injectable()
export class ApiService {
  //API_URL = 'https://localhost:44360/';
  API_URL = 'https://localhost:5001/';
  constructor(private httpClient: HttpClient) {}

  get(path: string, params?: any): Observable<any> {
    const headers = this.getHeaders();
    return this.httpClient.get(`${this.API_URL}${path}`, {
      headers,
      params: this.toHttpParams(params),
    });
  }

  /*
    getById(path: string, params?: any): Observable<any> {
      const headers = this.getHeaders();
      return this.httpClient.get(`${this.API_URL}${path}`, {
        headers,
        params: this.toHttpParams(params),
    });
}
*/

  /*
getById(path: string, params?: any): Observable<any>{
  //this._http.get<IProduct[]>
  const headers = this.getHeaders();
  return this.httpClient.get(`${this.API_URL}${path}`, {
    headers,
    params: this.toHttpParams(params),
}).mergeMap(item => item)
  .filter(item => item.movieId === params.id);
}
*/

  //https://forum.ionicframework.com/t/get-item-by-id-ionic/96157
  //https://www.techiediaries.com/ionic-http/
  //https://www.techiediaries.com/ionic-http-client/
  /* last
getMovieById(movieId: number): Observable<Movie> {
  // this.http.get('https://example.com/api/items').pipe(map(data => {})).subscribe(result => {
  //  console.log(result) });
  /*
  return  this.httpClient.get(this.API_URL + 'Movies/' + movieId)
  .map(response  => {
  return  new  Movie(response);
  }).catch((err)=>{
  console.error(err);
  });
*/
  /*
  return  this.httpClient.get(this.API_URL + 'Movies/' + movieId)
  .pipe(map(response  => {
  return  new  Movie(response);
  }));
}
*/
  /*
    itemsRouter.get("/:id", async (req: Request, res: Response) => {
  const id: number = parseInt(req.params.id, 10);

  try {
    const item: Item = await ItemService.find(id);

    if (item) {
      return res.status(200).send(item);
    }

    res.status(404).send("item not found");
  } catch (e) {
    res.status(500).send(e.message);
  }
});
    */

  post(path: string, body = {}): Observable<any> {
    const headers = this.getHeaders();
    return this.httpClient.post(
      `${this.API_URL}${path}`,
      JSON.stringify(body),
      { headers }
    );
  }

  put(path: string, body = {}): Observable<any> {
    const headers = this.getHeaders();
    return this.httpClient.put(`${this.API_URL}${path}`, JSON.stringify(body), {
      headers,
    });
  }

  delete(path: string, params?: any): Observable<any> {
    const headers = this.getHeaders();
    return this.httpClient.delete(`${this.API_URL}${path}`, {
      headers,
      params: this.toHttpParams(params),
    });
  }

  private getHeaders() {
    const headers = {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    } as any;

    return headers;
  }

  private toHttpParams(params): HttpParams {
    if (!params) {
      return new HttpParams();
    }
    return Object.getOwnPropertyNames(params).reduce(
      (p, key) => p.set(key, params[key]),
      new HttpParams()
    );
  }
}
