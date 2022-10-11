import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { LinksService } from 'src/app/services/links.service';
import { Login } from 'src/app/Login';
import { Router } from '@angular/router';
import { UiService } from 'src/app/services/ui.service';
import { Subscription } from 'rxjs';
import { User } from 'src/app/User';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  errorMessage: string = '';
  showErrorMessage: boolean = false;
  model: Login = { email: "", password: "" };
  showPassword: boolean = false;
  faEye = faEye;
  faEyeSlash = faEyeSlash;

  constructor(private accountService: AccountService,
    private router: Router,
    private uiService: UiService) { }

  ngOnInit(): void {
  }

  onSubmit() {
    this.showErrorMessage = false;

    // if (!this.email) {
    //   this.errorMessage = 'Please enter email!';
    //   this.showErrorMessage = true;
    //   return;
    // }

    // if (!this.password) {
    //   this.errorMessage = 'Please enter password!'
    //   this.showErrorMessage = true;
    //   return;
    // }

    // const loginForm: Login = {
    //   email: this.email,
    //   password: this.password
    // };

    this.accountService.login(this.model).subscribe(
      user => {
        this.uiService.setLoggedIn(true);
        this.uiService.setUsername(user.username);
        this.router.navigate(['']);
        return;
      },
      error => {
        this.errorMessage = `Error: ${error.statusText}`;
        this.showErrorMessage = true;
        console.log(error);
      }
    );

    this.showErrorMessage = false;
    this.model.email = '';
    this.model.password = '';
  }

  toggleShowPassword(): void {
    this.showPassword = !this.showPassword;
  }
}
