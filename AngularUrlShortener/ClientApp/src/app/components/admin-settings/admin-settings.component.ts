import { Component, OnInit } from '@angular/core';
import { ApiSettings } from '../../ApiSettings';
import { ApiSettingsService } from '../../services/api-settings.service';

@Component({
  selector: 'app-admin-settings',
  templateUrl: './admin-settings.component.html',
  styleUrls: ['./admin-settings.component.css']
})
export class AdminSettingsComponent implements OnInit {
  id: number = 1;
  hostname: string = '';
  allowSignup: boolean = false;
  allowAnonymousLinkCreation: boolean = false;

  constructor(private apiSettingsService: ApiSettingsService) { }

  ngOnInit(): void {
    this.apiSettingsService.getSettings().subscribe(
      settings => {
        this.id = settings.id;
        this.hostname = settings.hostname;
        this.allowSignup = settings.allowSignup;
        this.allowAnonymousLinkCreation = settings.allowAnonymousLinkCreation;
      },
      error => {
        console.log(error);
      });
  }

  onSubmit() {
    if (!this.hostname) {
      alert("Please enter a hostname!")
      return;
    }

    const apiSettings: ApiSettings = {
      id: this.id,
      hostname: this.hostname,
      allowSignup: this.allowSignup,
      allowAnonymousLinkCreation: this.allowAnonymousLinkCreation
    };

    this.apiSettingsService.updateSettings(apiSettings).subscribe(
      setting => {
        alert("Settings updated!");
      },
      error => {
        console.log(error);
      });
  }
}
