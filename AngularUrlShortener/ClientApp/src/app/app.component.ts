import { Component } from '@angular/core';
import { UiService } from 'src/app/services/ui.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  subscription!: Subscription;

  constructor(private uiService: UiService, private router: Router) {
    this.subscription = this.uiService.onToggleMenu().subscribe();

    // Hide nav menu.
    router.events.subscribe((val) => {
      this.uiService.hideNavMenu();
    });
  }

  onClick() {
    this.uiService.hideNavMenu();
    //this.uiService.setLoggedIn(true);
  }
}
