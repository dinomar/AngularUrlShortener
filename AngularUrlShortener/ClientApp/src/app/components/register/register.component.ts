import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { UiService } from 'src/app/services/ui.service';
import { User } from 'src/app/User';
import { Register } from 'src/app/Register';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  errorMessage: string = '';
  showErrorMessage: boolean = false;
  username: string = '';
  email: string = '';
  password: string = '';
  password2: string = '';
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

    if (!this.username) {
      this.errorMessage = 'Please enter a username';
      this.showErrorMessage = true;
    }

    if (!this.email) {
      this.errorMessage = 'Please enter a email!';
      this.showErrorMessage = true;
      return;
    }

    if (!this.password) {
      this.errorMessage = 'Please enter password!'
      this.showErrorMessage = true;
      return;
    }

    if (!this.password2) {
      this.errorMessage = 'Please enter password again!'
      this.showErrorMessage = true;
      return;
    }

    if (this.password !== this.password2) {
      this.errorMessage = 'Password does not match!';
      this.showErrorMessage = true;
      return;
    }

    const registerForm: Register = {
      username: this.username,
      email: this.email,
      password: this.password,
      password2: this.password2
    }

    this.accountService.register(registerForm).subscribe(
      user => {
        this.uiService.setLoggedIn(true);
        this.uiService.setUsername(user.username); // TODO: remove?
        this.router.navigate(['']);
        return;
      },
      error => {
        this.errorMessage = error.statusText;
        this.showErrorMessage = true;
        return;
      }
    );

    this.showErrorMessage = false;
    this.username = '';
    this.email = '';
    this.password = '';
    this.password2 = '';
    this.errorMessage = '';
  }

  toggleShowPassword(): void {
    this.showPassword = !this.showPassword;
  }
}
