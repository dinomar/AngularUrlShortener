import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { UiService } from 'src/app/services/ui.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit {

  constructor(private accountService: AccountService,
    private router: Router,
    private uiService: UiService) { }

  ngOnInit(): void {
    this.accountService.logout().subscribe(
      () => {
        this.uiService.setLoggedIn(false);
        this.uiService.setUsername('');
        this.router.navigate(['']);
        return;
      },
      error => {
        console.log(error);
        return;
      }
    );
  }

}
