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
  model: Register = { username: "", email: "", password: "", password2: "" };
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

    // if (!this.model.username) {
    //   this.errorMessage = 'Please enter a username';
    //   this.showErrorMessage = true;
    // }

    // if (!this.model.email) {
    //   this.errorMessage = 'Please enter a email!';
    //   this.showErrorMessage = true;
    //   return;
    // }

    // if (!this.model.password) {
    //   this.errorMessage = 'Please enter password!'
    //   this.showErrorMessage = true;
    //   return;
    // }

    // if (!this.model.password2) {
    //   this.errorMessage = 'Please enter password again!'
    //   this.showErrorMessage = true;
    //   return;
    // }

    if (this.model.password !== this.model.password2) {
      this.errorMessage = 'Password does not match!';
      this.showErrorMessage = true;
      return;
    }

    this.accountService.register(this.model).subscribe(
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
    this.model = { username: "", email: "", password: "", password2: "" };
    this.errorMessage = '';
  }

  toggleShowPassword(): void {
    this.showPassword = !this.showPassword;
  }
}
