import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class ModerationService {
  private apiUrl: string = 'http://localhost:4200/api/moderation';

  constructor(private httpClient: HttpClient) { }

  // get all links
  // get link
  // delete
}
