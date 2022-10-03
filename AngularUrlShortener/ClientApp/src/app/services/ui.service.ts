import { Injectable } from '@angular/core';
import { Observable, ObservedValueOf, Subject } from 'rxjs';
import { AccountService } from './account.service';
import { User } from 'src/app/User';

@Injectable({
  providedIn: 'root'
})
export class UiService {
  private showMenu: boolean = false;
  private loggedIn: boolean = false;
  private username: string = '';
  private menuSubject = new Subject<any>();
  private loggedInSubject = new Subject<any>();
  private usernameSubject = new Subject<any>();

  constructor(private accountService: AccountService) {
    this.accountService.getCurrentUser().subscribe(
      user => {
        this.setLoggedIn(true);
        this.setUsername(user.username);
      },
      error => {
        this.setLoggedIn(false);
        this.setUsername('');
      });
  }


  // Menu
  showNavMenu(): void {
    this.showMenu = !this.showMenu;
    this.menuSubject.next(this.showMenu);
  }

  // Menu
  hideNavMenu(): void {
    this.showMenu = false;
    this.menuSubject.next(this.showMenu);
  }

  onToggleMenu(): Observable<any> {
    return this.menuSubject.asObservable();
  }


  // Logged In
  onLoggedInChanged(): Observable<any> {
    return this.loggedInSubject.asObservable();
  }

  setLoggedIn(loggedIn: boolean) {
    this.loggedIn = loggedIn;
    this.loggedInSubject.next(this.loggedIn);
  }

  // Username
  setUsername(username: string) {
    this.username = username;
    this.usernameSubject.next(this.username);
  }

  onUsernameChanged(): Observable<any> {
    return this.usernameSubject.asObservable();
  }
}
