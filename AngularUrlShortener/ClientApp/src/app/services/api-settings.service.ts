import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiSettings } from 'src/app/ApiSettings';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class ApiSettingsService {
  private apiUrl: string = '';

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.apiUrl = baseUrl + 'api/settings';
  }

  getSettings(): Observable<ApiSettings> {
    return this.httpClient.get<ApiSettings>(this.apiUrl);
  }

  updateSettings(apiSettings: ApiSettings): Observable<ApiSettings> {
    return this.httpClient.put<ApiSettings>(this.apiUrl, apiSettings, httpOptions);
  }
}
