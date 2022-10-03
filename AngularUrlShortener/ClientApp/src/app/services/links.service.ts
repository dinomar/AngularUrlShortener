import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Link } from 'src/app/Link';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class LinksService {
  private apiUrl: string = 'http://localhost:4200/api/links';

  constructor(private httpClient: HttpClient) { }

  getUrl(shortUrl: string): Observable<Link> {
    const url = `${this.apiUrl}/${shortUrl}`;
    return this.httpClient.get<Link>(url);
  }

  createLink(link: Link): Observable<Link> {
    return this.httpClient.post<Link>(this.apiUrl, link, httpOptions);
  }

  getLinks(): Observable<Link[]> {
    return this.httpClient.get<Link[]>(this.apiUrl);
  }

  removeLink(link: Link): Observable<any> {
    const url = `${this.apiUrl}/${link.id}`;
    return this.httpClient.delete<any>(url);
  }
}
