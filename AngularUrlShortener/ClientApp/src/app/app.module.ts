import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { LinksComponent } from './components/links/links.component';
import { LoginComponent } from './components/login/login.component';
import { RedirectComponent } from './components/redirect/redirect.component';
import { RegisterComponent } from './components/register/register.component';
import { LinksItemComponent } from './components/links-item/links-item.component';
import { LogoutComponent } from './components/logout/logout.component';
import { AdminComponent } from './components/admin/admin.component';
import { AdminUsersComponent } from './components/admin-users/admin-users.component';
import { AdminLinksComponent } from './components/admin-links/admin-links.component';
import { AdminSettingsComponent } from './components/admin-settings/admin-settings.component';
import { AboutComponent } from './components/about/about.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';

const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'mylinks', component: LinksComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'logout', component: LogoutComponent },
  { path: 'about', component: AboutComponent },
  {
    path: 'admin', component: AdminComponent,
    children: [
      { path: '', redirectTo: 'settings', pathMatch: 'full' },
      { path: 'settings', component: AdminSettingsComponent },
      { path: 'users', component: AdminUsersComponent },
      { path: 'links', component: AdminLinksComponent }
    ]
  },
  { path: '**', component: RedirectComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    HeaderComponent,
    FooterComponent,
    LinksComponent,
    LoginComponent,
    RedirectComponent,
    RegisterComponent,
    LinksItemComponent,
    LogoutComponent,
    AboutComponent,
    AdminComponent,
    AdminUsersComponent,
    AdminLinksComponent,
    AdminSettingsComponent,
    CounterComponent,
    FetchDataComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(appRoutes),
    FontAwesomeModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
