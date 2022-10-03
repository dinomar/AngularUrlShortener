import { Component, OnInit, OnDestroy } from '@angular/core';
import { UiService } from 'src/app/services/ui.service';
import { filter, map, Subscription } from 'rxjs';
import { faBars } from '@fortawesome/free-solid-svg-icons';
import { NavigationEnd, NavigationStart, Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit, OnDestroy {
  title: string = "Url Shortener"
  faBars = faBars;
  showMenu: boolean = false;
  loggedIn: boolean = false;
  username: string = '';
  showCreateLink: boolean = false;
  menuSubscription!: Subscription;
  loggedInSubscription!: Subscription;
  usernameSubscription!: Subscription;
  routeSubscription!: Subscription;

  constructor(private uiService: UiService, private router: Router) {
    this.menuSubscription = this.uiService
      .onToggleMenu()
      .subscribe(value => this.showMenu = value);

    this.loggedInSubscription = this.uiService
      .onLoggedInChanged()
      .subscribe(value => this.loggedIn = value);

    this.usernameSubscription = this.uiService
      .onUsernameChanged()
      .subscribe(value => this.username = value);

    this.routeSubscription = this.router.events
      .pipe(
        filter(event => event instanceof NavigationStart),
        map(event => event as NavigationStart)
      )
      .subscribe(
        event => {
          if (event.url !== '/') {
            this.showCreateLink = true;
          } else {
            this.showCreateLink = false;
          }
        }
      );
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.routeSubscription.unsubscribe();
  }

  toggleShowMenu() {
    this.uiService.showNavMenu();
  }
}
