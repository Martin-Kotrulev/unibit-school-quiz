import { Http, RequestOptions, Headers } from '@angular/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

import { AuthService } from '../users/auth.service';

const baseUrl = 'http://localhost:5000';
const GET = 'GET';
const POST = 'POST';

@Injectable()
export class HttpService {
  constructor(
    private http: Http,
    private authService: AuthService
  ) {}

  get (url, auhtenticated = false) {
    const requestOptions = this.getRequestOptions(GET, auhtenticated);
    this.http
      .get(`${baseUrl}${url}`, requestOptions)
      .map((res: any) => res.json());
  }

  post(url, data, authenticated = false): Observable<any> {
    const requestOptions = this.getRequestOptions(POST, authenticated);
    return this.http
      .post(`${baseUrl}${url}`, JSON.stringify(data), requestOptions)
      .map((res: any) => res.json());
  }

  private getRequestOptions(method, authenticated): RequestOptions {
    const headers = new Headers();

    if (method === POST) {
      headers.append('Content-Type', 'application/json');
    }

    if (authenticated) {
      headers.append(
        'Authorization',
        `bearer ${this.authService.getToken()}`
      );
    }

    return new RequestOptions({ headers });
  }
}
