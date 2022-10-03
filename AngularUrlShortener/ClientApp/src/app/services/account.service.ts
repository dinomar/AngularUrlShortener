import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Register } from 'src/app/Register';
import { Login } from 'src/app/Login';
import { User } from '../User';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private apiUrl: string = 'http://localhost:4200/api/account';

  constructor(private httpClient: HttpClient) { }

  register(register: Register): Observable<User> {
    const url = `${this.apiUrl}/register`;
    return this.httpClient.post<User>(url, register, httpOptions);
  }

  login(login: Login): Observable<User> {
    const url = `${this.apiUrl}/login`;
    return this.httpClient.post<User>(url, login, httpOptions);
  }

  getCurrentUser(): Observable<User> {
    const url = `${this.apiUrl}/me`;
    return this.httpClient.get<User>(url);
  }

  logout(): Observable<any> {
    const url = `${this.apiUrl}/logout`;
    return this.httpClient.get(url);
  }

  getUsers(): Observable<User[]> {
    return this.httpClient.get<User[]>(this.apiUrl);
  }

  banUser(userId: string): Observable<any> {
    const url = `${this.apiUrl}/disable/${userId}`;
    return this.httpClient.get(url);
  }
}
